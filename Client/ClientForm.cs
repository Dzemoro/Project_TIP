using ClientClassLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net.Sockets;
using NAudio.Wave;
using System.Media;

namespace ClientApp
{
    
    public partial class ClientForm : Form
    {
       Client client = new Client();
        public ClientForm()
        {
            InitializeComponent();
            this.client = new Client();
        }
        public delegate void delUpdateBox(string text);
        public bool recording = false;

        ThreadStart threadStart,audThreadStart,sigThreadStart;
        Thread receiverThread, audioThread, sigThread;
        WaveIn inputRec;
        BufferedWaveProvider outputWaveProvider;
        WaveOut player = new WaveOut();

        private void ClientForm_Load(object sender, EventArgs e)
        {
            threadStart = new ThreadStart(StartListener);
            audThreadStart = new ThreadStart(ReceiveTransmition);
            receiverThread = new Thread(threadStart);
            audioThread = new Thread(audThreadStart);
          
            receiverThread.Name = "Receiver Thread";
            receiverThread.Start();
            audioThread.Start();
        }
        #region Interface_Handling     
        private void SendButton_Click(object sender, EventArgs e)
        {
            if (addressbox.Text.Length != 0 && addressbox.Text.Length <= 15)
            {
                if ((new Regex(@"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$")).IsMatch(addressbox.Text))
                {
                    addressbox.ForeColor = Color.Green;
                    String temp = addressbox.Text;
                    IPAddress ipaddress = IPAddress.Parse(temp);
                    if (message.Text.Length == 0)
                    {
                        message.Text = "PING";
                        client.sendMessage(ipaddress, "PING");
                    };
                    temp = message.Text;
                    client.sendMessage(ipaddress, temp);
                    sigThreadStart= new ThreadStart(PlayCall);
                    sigThread = new Thread(sigThreadStart);
                    sigThread.Start();

                }
                else
                {
                    addressbox.ForeColor = Color.Red;
                    MessageBox.Show("Invalid address");
                }
            }
            else
            {
                MessageBox.Show("Invalid address");
            }
        }
        private void UpdateBox(string text)
        {
            this.label3.Text = text;
        }
        private void Refresh_Click(object sender, EventArgs e) 
        {
            List<WaveInCapabilities> audioSources = new List<WaveInCapabilities>();
            for(int i=0;i< WaveIn.DeviceCount;i++)
            {
                audioSources.Add(WaveIn.GetCapabilities(i));
            }
            audioSList.Items.Clear();
            foreach(var source in audioSources)
            {
                ListViewItem device = new ListViewItem(source.ProductName);
                device.SubItems.Add(new ListViewItem.ListViewSubItem(device, source.Channels.ToString()));
                audioSList.Items.Add(device);
            }
        }  
        private void StartSbutton_Click(object sender, EventArgs e) 
        {
            if(recording==false)
            {
                if (audioSList.SelectedItems.Count == 0) return;
                recording = true;
                int deviceNumber = audioSList.SelectedItems[0].Index;
                inputRec = new WaveIn
                {
                    WaveFormat = new WaveFormat(44100, WaveIn.GetCapabilities(deviceNumber).Channels)
                };
                inputRec.DataAvailable += RecorderOnDataAvailable;
                outputWaveProvider = new BufferedWaveProvider(inputRec.WaveFormat);

                player = new WaveOut
                {
                    DeviceNumber = 0
                };
                player.Init(outputWaveProvider);
                player.Play();
                inputRec.StartRecording(); 
            }

            else
            {
                inputRec.StopRecording();
                player.Stop();
                player.Dispose();
                inputRec.Dispose();
                outputWaveProvider.ClearBuffer();
                recording = false;
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
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
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
        private void StartListener()
        {
            int listenPort = 11110;
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            try
            {
                while (true)
                {
                    byte[] bytes = listener.Receive(ref groupEP);
                    delUpdateBox updateBox = new delUpdateBox(UpdateBox);
                    this.label3.BeginInvoke(updateBox, $" {Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");
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
        #endregion 
        #region Audio_Management
        /// <summary>
        /// Method to play internal call sound
        /// </summary>
        private void PlayCall()
        {
            int i = 0;
            SoundPlayer simpleSound;
            while (i<3)
               {
                    simpleSound = new SoundPlayer(@"C:\Users\Krzysiek\Desktop\Sounds\internal.wav");
                    simpleSound.Play();
                    Thread.Sleep(11500);
                    i += 1;
               }
            
                  

        }
        /// <summary>
        /// Method to play ring sound
        /// </summary>
        private void PlayRing()
        {
            SoundPlayer simpleSound;
            simpleSound = new SoundPlayer(@"C:\Users\Krzysiek\Desktop\Sounds\call_ring.wav");
            simpleSound.Play();
        }
        private void WOut_PlaybackStopped(object sender, NAudio.Wave.StoppedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void RecorderOnDataAvailable(object sender, WaveInEventArgs waveInEventArgs)
        {
            client.sendBytes(IPAddress.Parse("127.0.0.1"), waveInEventArgs.Buffer);
        }
        #endregion

    }
}
