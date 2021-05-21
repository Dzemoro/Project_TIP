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
    public partial class SessionForm : Form
    {
        int sendport,listenPort;
        string nickName,udpAddress;
        bool recording = false;
        Client client = new Client();
        
        public SessionForm(int port, string nickname, string udpaddress, int listenport)
        {
            InitializeComponent();
            this.sendport = port;
            this.nickName = nickname;
            this.udpAddress = udpaddress;
            label1.Text = "Talking to: " + nickName;
            this.listenPort = listenport;
            var task = Task.Run(ReceiveTransmition);

        }
        private void discButton_Click(object sender, EventArgs e)
        {

        }
        WaveIn inputRec;
        BufferedWaveProvider outputWaveProvider;
        WaveOut player = new WaveOut();

        public SessionForm()
        {
            InitializeComponent();
        }

        private void startSbutton_Click(object sender, EventArgs e)
        {
            if (recording == false)
            {
                //Zmiana przycisku
                this.startSbutton.Text = "Mute";
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
                this.startSbutton.Text = "Unmute";
                inputRec.StopRecording();
                player.Stop();
                player.Dispose();
                inputRec.Dispose();
                outputWaveProvider.ClearBuffer();
                recording = false;
            }
        }
        private void RecorderOnDataAvailable(object sender, WaveInEventArgs waveInEventArgs)
        {
            client.sendBytes(IPAddress.Parse(udpAddress), waveInEventArgs.Buffer);
        }
        private void refreshB_Click(object sender, EventArgs e)
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
            
            
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, 0);
            UdpClient listener = new UdpClient(groupEP.Port);
            try
            {
                while (true)
                {
                    byte[] bytes = listener.Receive(ref groupEP);
                    //Console.WriteLine("Received Something");
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


    }
   

}
