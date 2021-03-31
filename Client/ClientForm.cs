using ClientClassLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net.Sockets;
using NAudio;

namespace ClientApp
{
    public partial class ClientForm : Form
    {
        Client client = new Client();
       // NAudio.Wave.BufferedWaveProvider;
        public ClientForm()
        {
            InitializeComponent();
            this.client = new Client();

        }
        public delegate void delUpdateBox(string text);
        public bool recording = false;

        ThreadStart threadStart;
        Thread receiverThread;

        private NAudio.Wave.WaveIn sourceStream = null;
        private NAudio.Wave.DirectSoundOut waveOut = null;

        private void button1_Click(object sender, EventArgs e)
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

        private void ClientForm_Load(object sender, EventArgs e)
        {
            threadStart = new ThreadStart(StartListener);
            receiverThread = new Thread(threadStart);
            receiverThread.Name = "Receiver Thread";
            receiverThread.Start();
        }
        public void StartListener()
        {
            int listenPort = 11000;
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

            try
            {
                while (true)
                {
                    byte[] bytes = listener.Receive(ref groupEP);
                    //  Console.WriteLine($"Received broadcast from {groupEP} :");
                    // Console.WriteLine($" {Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");
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
        public void UpdateBox(string text)
        {
            this.label3.Text = text;
        }

        private void button1_Click_1(object sender, EventArgs e) //Wybór urządzenia wejściowego
        {
            List<NAudio.Wave.WaveInCapabilities> audioSources = new List<NAudio.Wave.WaveInCapabilities>();

            for(int i=0;i< NAudio.Wave.WaveIn.DeviceCount;i++)
            {
                audioSources.Add(NAudio.Wave.WaveIn.GetCapabilities(i));
            }
            audioSList.Items.Clear();
            foreach(var source in audioSources)
            {
                ListViewItem device = new ListViewItem(source.ProductName);
                device.SubItems.Add(new ListViewItem.ListViewSubItem(device, source.Channels.ToString()));
                audioSList.Items.Add(device);
            }

        }

        private void startSButton_Click(object sender, EventArgs e) //Nagrywanie i przetywanie nagrywania
        {
            if(recording==false)
            {
                if (audioSList.SelectedItems.Count == 0) return;
                int deviceNumber = audioSList.SelectedItems[0].Index;

                sourceStream = new NAudio.Wave.WaveIn();
                sourceStream.DeviceNumber = deviceNumber;
                sourceStream.WaveFormat = new NAudio.Wave.WaveFormat(44100, NAudio.Wave.WaveIn.GetCapabilities(deviceNumber).Channels);
                //
                byte[] buffer = new byte[44100];
                //
                NAudio.Wave.WaveInProvider waveProv = new NAudio.Wave.WaveInProvider(sourceStream);
                waveOut = new NAudio.Wave.DirectSoundOut();
               
                waveOut.Init(waveProv);
                sourceStream.StartRecording();
                waveProv.Read(buffer, 0, 44100);
              
                waveOut.Play();
                recording = true;
            }
            else
            {
                
                if(waveOut!=null)
                {
                    waveOut.Stop();
                    waveOut.Dispose();
                    waveOut = null;
                }
                if( sourceStream !=null)
                {
                    sourceStream.StopRecording();
                    sourceStream.Dispose();
                    sourceStream = null;

                }
                recording = false;
            }
        }
           
    }
}
