using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using NAudio.Wave;
using System.Security.Cryptography;

namespace ClientClassLib
{
    public class Client
    {
        public Client() { }
        public void sendMessage(IPAddress address,String msg, int port)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
           // IPAddress address = IPAddress.Parse("127.0.0.1");
           
            byte[] sendbuf = Encoding.ASCII.GetBytes(msg);
            IPEndPoint ep = new IPEndPoint(address, port);
            s.SendTo(sendbuf, ep);
            Console.WriteLine("Message sent to the address");
        }
        public void sendBytes(IPAddress address, byte[] bytes, int port)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);   
            IPEndPoint ep = new IPEndPoint(address, port);
            s.SendTo(bytes, ep);
           
        }


    }
    public class SavingWaveProvider : IWaveProvider, IDisposable
    {
        private readonly IWaveProvider sourceWaveProvider;
        private readonly WaveFileWriter writer;
        private bool isWriterDisposed;

        public SavingWaveProvider(IWaveProvider sourceWaveProvider, string wavFilePath)
        {
            this.sourceWaveProvider = sourceWaveProvider;
            writer = new WaveFileWriter(wavFilePath, sourceWaveProvider.WaveFormat);
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            var read = sourceWaveProvider.Read(buffer, offset, count);
            if (count > 0 && !isWriterDisposed)
            {
                writer.Write(buffer, offset, read);
            }
            if (count == 0)
            {
                Dispose(); // auto-dispose in case users forget
            }
            return read;
        }

        public WaveFormat WaveFormat { get { return sourceWaveProvider.WaveFormat; } }

        public void Dispose()
        {
            if (!isWriterDisposed)
            {
                isWriterDisposed = true;
                writer.Dispose();
            }
        }
    }
}
