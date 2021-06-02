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
//CZY KLIENT ODBIERA
//CZY DRUGI KLIENT WYSYŁA
namespace ClientApp
{
    public partial class SessionForm : Form
    {
        int sendport,listenPort;
        CancellationTokenSource listenerCancellationToken = new CancellationTokenSource();
        string nickName,udpAddress;
        bool recording = false;
        Client client = new Client();
        bool first = true;
        string username;
        WaveIn inputRec;
        BufferedWaveProvider outputWaveProvider= new BufferedWaveProvider(new WaveFormat(44100, WaveIn.GetCapabilities(0).Channels));
        WaveOut player = new WaveOut();
        TcpClient tcpClient = new TcpClient();
        NetworkStream stream;
        public SessionForm(int port, string nickname, string udpaddress, int listenport, TcpClient tcpClient,string username)
        {
            InitializeComponent();
            Refresh();
            this.sendport = port;
            this.nickName = nickname;
            this.udpAddress = udpaddress;
            //this.udpAddress = "192.168.0.184";
            label1.Text = "Talking to: " + nickName;
            this.listenPort = listenport;
            this.username = username;
            outputWaveProvider = new BufferedWaveProvider(new WaveFormat(44100, WaveIn.GetCapabilities(0).Channels));
            player.Init(outputWaveProvider);
            
            var task = Task.Run(ReceiveTransmition);
            player.Play();
            StartTCPListener();
            Console.WriteLine("Receiving at port:" + listenport);
            try
            {
                this.tcpClient = tcpClient;
                stream = tcpClient.GetStream();
                //StartTCPListener();
            }
            catch (SocketException se)
            {
                MessageBox.Show("Connection Lost");
                ConnectionPanel connectionPanel = new ConnectionPanel();
                connectionPanel.Show();
               // 
                this.Close();
            }

        }
        private void discButton_Click(object sender, EventArgs e)
        {
            var msg = "HANG:"+username+":"+nickName;// "CONN:" + words[2] + ":" + listenport.ToString();
            var data = System.Text.Encoding.ASCII.GetBytes(msg);
            stream.Write(data, 0, data.Length);
            if (this.startSbutton.Text == "Mute")
            {
                inputRec.StopRecording();
            }
            player.Stop();
            player.Dispose();
            inputRec.Dispose();
            outputWaveProvider.ClearBuffer();
            this.Close();
        }
        

        public SessionForm()
        {
            InitializeComponent();
        }

        public void Listen()
        {

            while (true)
            {
                var data = new Byte[256];
                String responseData = String.Empty;

                var bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine(responseData);

               if(responseData.Contains("HANG"))
               {
                    this.Invoke((MethodInvoker)delegate
                    { 
                        this.Close();
                    });
                }
            }
        }
        private void startSbutton_Click(object sender, EventArgs e)
        {
            if(first)
            {
                outputWaveProvider.ClearBuffer();
                this.startSbutton.Text = "Mute";
                if (audioSList.SelectedItems.Count == 0) return;
                
                int deviceNumber = audioSList.SelectedItems[0].Index;
                inputRec = new WaveIn
                {
                    WaveFormat = new WaveFormat(44100, WaveIn.GetCapabilities(deviceNumber).Channels)
                };
                inputRec.DataAvailable += RecorderOnDataAvailable;
                

                player = new WaveOut
                {
                    DeviceNumber = 0
                };
                first = false;
            }
           

            if (recording == false)
            {
                //Zmiana przycisku
                recording = true;
                this.startSbutton.Text = "Mute";
                inputRec.StartRecording();
            }

            else
            {
                this.startSbutton.Text = "Unmute";
                inputRec.StopRecording();
                recording = false;
            }
        }
        private void RecorderOnDataAvailable(object sender, WaveInEventArgs waveInEventArgs)
        {
            client.sendBytes(IPAddress.Parse(udpAddress), waveInEventArgs.Buffer,sendport);
            Console.WriteLine("Sending data");
            
        }
        
        private void refreshB_Click(object sender, EventArgs e)
        {
            Refresh();
        }
        private void Refresh()
        {
            List<WaveInCapabilities> audioSources = new List<WaveInCapabilities>();
            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                audioSources.Add(WaveIn.GetCapabilities(i));
            }
            audioSList.Items.Clear();
            foreach (var source in audioSources)
            {
                ListViewItem device = new ListViewItem(source.ProductName);
                device.SubItems.Add(new ListViewItem.ListViewSubItem(device, source.Channels.ToString()));
                audioSList.Items.Add(device);
            }
        }
        
        private void ReceiveTransmition()
        {

            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            UdpClient listener = new UdpClient(groupEP.Port);
            try
            {
                while (true)
                {
                    byte[] bytes = listener.Receive(ref groupEP);
                    Console.WriteLine("Received Something");
                    try
                    {
                        outputWaveProvider.AddSamples(bytes, 0, bytes.Length);
                    }
                    catch(System.InvalidOperationException e)
                    {
                        outputWaveProvider.ClearBuffer();
                      if( player.PlaybackState==PlaybackState.Stopped)
                      {
                            player.Play();
                      }
                      if(player.PlaybackState == PlaybackState.Paused)
                      {
                            player.Resume();
                      }

                    }
                   
                    
                }
            }
            catch (System.InvalidOperationException e)
            {
                Console.WriteLine(e);
            }
            catch(Exception e)
            {
                outputWaveProvider.ClearBuffer();
            }
            finally
            {
                listener.Close();
            }
        }
        private void StartTCPListener()
        {
            var task = Task.Run(() => Listen(), listenerCancellationToken.Token); 
        }
    }
   

}
