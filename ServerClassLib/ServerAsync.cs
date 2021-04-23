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
            byte[] buffer = new byte[1024];
            string portResponse = "PORT:";
            string listResponse = "LIST";
            string decline = "NACK";

            byte[] declineByte = new ASCIIEncoding().GetBytes(decline);

            while (true)
            {
                var data = GetData(stream, buffer);
                if(data != null)
                {
                    if (data[0] == "HELL")
                    {
                        if(data.Length < 4)
                        {
                            Console.WriteLine("Invalid data");
                            stream.Write(declineByte, 0, declineByte.Length);
                        }
                        else
                        {
                            Console.WriteLine(data[3]+" connected");
                            users.Add(data[3], data[1]);
                            int portNumber = FreeTcpPort();
                            portResponse += portNumber.ToString();
                            byte[] portResponseByte = new ASCIIEncoding().GetBytes(portResponse);
                            stream.Write(portResponseByte, 0, portResponseByte.Length);
                            portResponse = "PORT:";
                        }
                    }
                    else if (data[0] == "LIST")
                    {
                        if (data.Length < 2)
                        {
                            Console.WriteLine("Invalid data");
                            stream.Write(declineByte, 0, declineByte.Length);
                        }
                        else
                        {
                            foreach (string key in users.Keys)
                            {
                                if (key != data[1])
                                {
                                    listResponse += ":" + key;
                                }
                            }
                            byte[] listResponseByte = new ASCIIEncoding().GetBytes(listResponse);
                            stream.Write(listResponseByte, 0, listResponseByte.Length);
                            listResponse = "LIST";
                        }
                    }
                }
            }
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
            string msg = ByteToString(stream, buffer);
            var temp = msg.Split(':');
            return temp;
        }
        public override void Start()
        {
            StartListening();
            AcceptClient();
        }
    }
}
