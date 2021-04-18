using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerClassLib
{
    public class ServerAsync : AbstractServer
    {
        Dictionary<string, string> users;

        public delegate void TransmissionDataDelegate(NetworkStream stream);
        public ServerAsync(string IPAddress = "127.0.0.1", int port = 8001) : base(System.Net.IPAddress.Parse(IPAddress), port)
        {
            users = new Dictionary<string, string>();
        }
        protected override void AcceptClient()
        {
            while(true)
            {
                TcpClient tcpClient = TcpListener.AcceptTcpClient();
                Stream = tcpClient.GetStream();
                TransmissionDataDelegate transmissionDelegate = new TransmissionDataDelegate(BeginDataTransmission);
                transmissionDelegate.BeginInvoke(Stream, TransmissionCallback, tcpClient);
            }
        }

        private void TransmissionCallback(IAsyncResult ar)
        {
        }

        protected override void BeginDataTransmission(NetworkStream stream)
        {
            string portResponse = "PORT:";

            byte[] buffer = new byte[1024];
            Console.WriteLine("Working");
            var data = GetData(stream, buffer);
            users.Add(data[3], data[1]);
            int portNumber = FreeTcpPort();
            portResponse += portNumber.ToString();
            byte[] portResponseByte = new ASCIIEncoding().GetBytes(portResponse);
            stream.Write(portResponseByte, 0, portResponseByte.Length);

        }
        static int FreeTcpPort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
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
            string[] result = new string[4];
            string msg = ByteToString(stream, buffer);
            result = msg.Split(':');
            return result;
        }
        public override void Start()
        {
            StartListening();
            AcceptClient();
        }
    }
}
