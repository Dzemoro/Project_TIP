using ClientClassLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using NAudio.Wave;
using System.Media;

namespace ClientApp
{
    
    public partial class ClientForm : Form
    {
        public delegate void TransmissionDataDelegate(NetworkStream stream);
        Client client = new Client();
        CancellationTokenSource listenerCancellationToken = new CancellationTokenSource();
        CancellationTokenSource playerCancellationToken = new CancellationTokenSource();
        NetworkStream stream;
        SoundPlayer simpleSound = new SoundPlayer();
        SoundPlayer singalSound = new SoundPlayer();
        int listenport;
        string callUser;
       //Task backgroundTask;
        public ClientForm()
        {
            InitializeComponent();
            this.client = new Client();
        }
        public ClientForm(int port, string ipAddress, string username,TcpClient tcpClient)
        {
            InitializeComponent();
            this.client = new Client();
            this.port = port;
            this.ipAddress = ipAddress;
            this.username = username;
            this.listenport = 0;
            try
            {
                this.tcpClient = tcpClient;
                stream = tcpClient.GetStream();
                StartTCPListener();
            }
            catch (SocketException se)
            {
                MessageBox.Show("Connection Lost");
                ConnectionPanel connectionPanel = new ConnectionPanel();
                connectionPanel.Show();
                this.Close();
            }
        }
        private int port;
        private string ipAddress,username;
        private TcpClient tcpClient;
        public delegate void delUpdateBox(string text);
        public delegate void delUpdateUsers(string text);
        public bool recording = false;

        ThreadStart threadStart,audThreadStart,sigThreadStart, tcpListeningThreadStart;
        Thread receiverThread, audioThread, sigThread, tcpListeningThread;
        WaveIn inputRec;
        BufferedWaveProvider outputWaveProvider;
        WaveOut player = new WaveOut();

        private void ClientForm_Load(object sender, EventArgs e)
        {
           //TEST
            threadStart = new ThreadStart(StartUdpListener);
            audThreadStart = new ThreadStart(ReceiveTransmition);
            receiverThread = new Thread(threadStart);
            audioThread = new Thread(audThreadStart);
          
            receiverThread.Name = "Receiver Thread";
          //  receiverThread.Start();
           // audioThread.Start();
        }
        #region Interface_Handling     
       
        private void UpdateBox(string text)
        {
            //this.label3.Text = text;
        }
        private void UpdateList(string text)
        {
            string[] words = text.Split(':');
           // users.Items.Add("Perry the platypus");
            users.Items.Clear();
            if(words.Length==1)
            {
                users.Items.Add("empty");
                return;
            }
            for(int i=1;i<words.Length;i++)
            {
                if(!words[i].Equals(username))
                {
                    users.Items.Add(words[i]);
                }
            }

        }
        private void GetList()
        {
            delUpdateBox delUpdateBox;
            while (true)
            {
                String msg = "LIST:" + this.username;
                Console.WriteLine(msg);
                delUpdateBox = new delUpdateBox(UpdateList);
                NetworkStream stream = tcpClient.GetStream();
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);
               
                stream.Write(data, 0, data.Length);
                data = new Byte[512];
                String responseData = String.Empty;
                //users.Items.Add("Perry the platypus");
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Response:" + responseData);
                this.users.BeginInvoke(delUpdateBox, responseData);
                Thread.Sleep(5000);
        
            }


        }
        private void UpdateThisList()
        {
            var delUpdateBox = new delUpdateBox(UpdateList);
            var data = new Byte[512];
            var responseData = String.Empty;
            NetworkStream stream = tcpClient.GetStream();
            users.Items.Add("Perry the platypus");
            var bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Response:" + responseData);
            this.users.BeginInvoke(delUpdateBox, responseData);
        }

        public void Listen()
        {
            
            while (true)
            {
                var data = new Byte[256];
                String responseData = String.Empty;

                var bytes= stream.Read(data,0,data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                string[] words = responseData.Split(':');
                Console.WriteLine(responseData);
                
                if (words[0]=="CALL")
                {
                    //CALL:Korzych:Dellor:127.0.0.1:8001 - Serwer->Korzych	

                    string message = words[2]+" wants to talk. Do you want to accept?";
                    string caption = "Incoming Call";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

                    // If the no button was pressed ...
                    if (result == DialogResult.No)
                    {
                        simpleSound.Stop();

                        // cancel the closure of the form.
                        var msg = "DENY:"+words[2];
                        data = System.Text.Encoding.ASCII.GetBytes(msg);
                        stream.Write(data, 0, data.Length);
                    }
                    else
                    {
                        //CALL:Korzych:Dellor:127.0.0.1:8001
                        simpleSound.Stop();
                        listenport = FreePort();
                        var udpPort = Int32.Parse(words[4]);
                        var nickname = words[2];
                        var udpAddress = words[3];
                        //UdpClient listener = new UdpClient(0)
                        var msg = "CONN:" + words[2] + ":"+listenport.ToString();
                        data = System.Text.Encoding.ASCII.GetBytes(msg);
                        stream.Write(data, 0, data.Length);

                        SessionForm sessionForm = new SessionForm(udpPort,nickname,udpAddress, listenport,tcpClient,username);
                        GuiClient.RunPanel(sessionForm);
                       

                    }
                }
                else if(words[0]=="LIST")
                {

                    var delUpdateBox = new delUpdateBox(UpdateList);
                    this.users.BeginInvoke(delUpdateBox, responseData);
                }
                else if (words[0] == "CONN")
                {
                    //CONN:DELLOR:127.0.0.1:2001- Connect - Serwer->Dellor
                   // listenport = FreePort();
                    var nickname = words[1];
                    var udpAddress = words[2];
                    var udpPort = Int32.Parse(words[3]);
                    //var msg = "CONN:" + words[2] + ":"+listenport.ToString();
                    singalSound.Stop();
                    simpleSound.Stop();
                    
                    SessionForm sessionForm = new SessionForm(udpPort, callUser, udpAddress, listenport, tcpClient,username);
                    GuiClient.RunPanel(sessionForm);

                }
                else if (words[0] == "DENY")
                {
                    MessageBox.Show("User Rejected Your Call"); 
                }
            }
        }

        #endregion
        #region Packet_Handling
        /// <summary>
        /// Method called to receive Audio data via UDP packets on designated port.
        /// </summary>
        private void ReceiveTransmition()
        {
            int listenPort = 11000;
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                while (true)
                {
                    byte[] bytes = listener.Receive(ref groupEP);
                    Console.WriteLine("Received Something");
                    outputWaveProvider.AddSamples(bytes, 0, bytes.Length);      
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Close();
            }
        }
        /// <summary>
        /// Method used to start UDP listener to receive data on designated port. 
        /// </summary>
        private void StartUdpListener()
        {//DO SESJI
            int listenPort = 11110;
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            try
            {
                while (true)
                {
                    byte[] bytes = listener.Receive(ref groupEP);
                    delUpdateBox updateBox = new delUpdateBox(UpdateBox);
                   // this.label3.BeginInvoke(updateBox, $" {Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Close();
            }
        }
        private void StartTCPListener()
        {
            var task = Task.Run(() => Listen(),listenerCancellationToken.Token); //listenerCancellationToken.Token);
        }
        private void TransmissionCallback(IAsyncResult ar)
        {
        }
        string ByteToString(NetworkStream stream, byte[] buffer)
        {
            char[] trim = { (char)0x0 };

            int len = stream.Read(buffer, 0, buffer.Length);
            if (Encoding.ASCII.GetString(buffer, 0, len) == "\r\n")
            {
                stream.Read(buffer, 0, buffer.Length);
            }
            string resultText = Encoding.ASCII.GetString(buffer).Trim(trim);
            Array.Clear(buffer, 0, buffer.Length);

            return resultText;
        }
        private string[] GetData(NetworkStream stream, byte[] buffer)
        {
            string msg = ByteToString(stream, buffer);
            var temp = msg.Split(':');
            return temp;
        }
        #endregion
        #region Cryptography
        private byte[] Encode(byte[] bytes)
        {
            byte[] bt= new byte[bytes.Length];
            return bt;
        }
        private byte[] Decode(byte[] bytes)
        {
            byte[] bt = new byte[bytes.Length];
            return bt;
        }

        private void callButton_Click(object sender, EventArgs e)
        {
            const string message ="Are you sure that you would like to call that user?";
            
            const string caption = "Calling";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {
                
            }
            else
            {
                listenport = FreePort();
                callUser = users.SelectedItem.ToString();
                String msg = "CALL:"+users.SelectedItem.ToString() +":"+ username  +  ":"+listenport.ToString();
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);
                stream.Write(data, 0, data.Length);
                Console.WriteLine(msg);
                var task = Task.Run(PlayCall, playerCancellationToken.Token);
                
              
            }
        }

        protected void BeginDataTransmission(NetworkStream stream)
        {
            byte[] buffer = new byte[1024];
            string callResponse = "CALL:";
            string list = "LIST";
            string decline = "NACK";
            byte[] declineByte = new ASCIIEncoding().GetBytes(decline);

            while (true)
            {
                var data = GetData(stream, buffer);
                if (data != null)
                {
                   if (data[0] == "LIST")
                    {
                        Console.WriteLine("LIST");
                        
                    }
                   else if(data[0]== "CALL")
                    {
                        //PlayRing();
                        Console.WriteLine("CALL");
                    }
                   else if (data[0] == "NACK")
                    {
                        Console.WriteLine("NACK");
                    }
                   else if (data[0]== "PING")
                    {
                        Console.WriteLine("Pong");
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var msg = "EXIT:" + username;// "CONN:" + words[2] + ":" + listenport.ToString();
            var data = System.Text.Encoding.ASCII.GetBytes(msg);
            stream.Write(data, 0, data.Length);
            Application.Exit();
        }
        #endregion

        #region Audio_Management
        /// <summary>
        /// Method to play internal call sound
        /// </summary>
        private void PlayCall()
        {
                    singalSound = new SoundPlayer(@"C:\Users\Krzysiek\Desktop\Sounds\internal.wav");
                    singalSound.Play();
        }
        /// <summary>
        /// Method to play ring sound
        /// </summary>
        private void PlayRing()
        {
  
            simpleSound = new SoundPlayer(@"C:\Users\Krzysiek\Desktop\Sounds\call_ring.wav");
            simpleSound.Play();
        }
        private void WOut_PlaybackStopped(object sender, NAudio.Wave.StoppedEventArgs e)
        {
            throw new NotImplementedException();
        }
      //DO ZMIANY
        private void RecorderOnDataAvailable(object sender, WaveInEventArgs waveInEventArgs)
        {
           // client.sendBytes(IPAddress.Parse("127.0.0.1"), waveInEventArgs.Buffer);
        }
        #endregion
   
        private void enableCall()
        {
            if(!callButton.Enabled)
            {
                callButton.Enabled = true;
            }
        }
        private IPAddress getIPAddress()
        {
            String strHostName = string.Empty;
            strHostName = Dns.GetHostName();
            IPHostEntry ipHostEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] address = ipHostEntry.AddressList;
            for (int i = 0; i < address.Length; i++)
            {
                var stringAddress = address[i].ToString();
                if (!stringAddress.Contains(":"))
                {
                    return address[i];
                }

            }
            return address[0];

        }
        static int FreePort()
        {
            var udp = new UdpClient(0, AddressFamily.InterNetwork);
            int port = ((IPEndPoint)udp.Client.LocalEndPoint).Port;
            udp.Dispose();
            return port;
        }
    }
}
