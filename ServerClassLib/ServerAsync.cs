using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerClassLib
{
    public class ServerAsync : AbstractServer
    {
        Dictionary<string, string> users;
        Dictionary<string, string> callMessages;

        public delegate void TransmissionDataDelegate(NetworkStream stream);
        public ServerAsync(string IPAddress = "127.0.0.1", int port = 8001) : base(System.Net.IPAddress.Parse(IPAddress), port)
        {
            users = new Dictionary<string, string>();
            callMessages = new Dictionary<string, string>();
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
            string calling = "CALL:";
            int listCounter = 0;
            string name = "";
            string callName = "";

            byte[] declineByte = new ASCIIEncoding().GetBytes(decline);

            while (true)
            {
                var data = GetData(stream, buffer);
                if (data != null)
                {
                    if (data[0] == "HELL")
                    {
                        if (data.Length < 4)
                        {
                            Console.WriteLine("Invalid data: HELL");
                            stream.Write(declineByte, 0, declineByte.Length);
                        }
                        else
                        {
                            name = data[3];
                            Console.WriteLine(name + " connected");
                            users.Add(name, data[1]);
                            listCounter = users.Count;
                            int portNumber = FreeTcpPort();
                            portResponse += portNumber.ToString();
                            byte[] portResponseByte = new ASCIIEncoding().GetBytes(portResponse);
                            stream.Write(portResponseByte, 0, portResponseByte.Length);
                            portResponse = "PORT:";
                            SendList(stream);
                        }
                    }
                    else if (data[0] == "CALL")
                    {
                        if (data.Length < 4)
                        {
                            Console.WriteLine("Invalid data: CALL");
                            stream.Write(declineByte, 0, declineByte.Length);
                        }
                        else
                        {
                            callMessages.Add(data[1], data[2] + ":" + data[3]);
                        }
                    }
                    else if (data[0] == "CONN")
                    {
                        if (data.Length < 3)
                        {
                            Console.WriteLine("Invalid data: CONN");
                            stream.Write(declineByte, 0, declineByte.Length);
                        }
                        else
                        {
                            
                        }
                    }
                }
                if (listCounter != users.Count && listCounter != 0)
                {
                    Console.WriteLine("Wysylam do: " + name);
                    SendList(stream);
                    Console.WriteLine("Wyslalem");
                    listCounter = users.Count;
                }
                if(callMessages.ContainsKey(name))
                {
                    //CALL:KEY:VALUE
                    var nickWithPort = callMessages[name].Split(':');
                    calling += name + ":" + nickWithPort[0] + ":" + users[nickWithPort[0]] + ":" + nickWithPort[1];
                    byte[] callingByte = new ASCIIEncoding().GetBytes(calling);
                    stream.Write(callingByte, 0, callingByte.Length);
                    callMessages.Remove(name);
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
            try
            {
                char[] trim = { (char)0x0 };
                if(stream.DataAvailable)
                {
                    int len = stream.Read(buffer, 0, buffer.Length);
                    if (Encoding.ASCII.GetString(buffer, 0, len) == "\r\n")
                    {
                        stream.Read(buffer, 0, buffer.Length);
                    }
                    string resultText = Encoding.ASCII.GetString(buffer).Trim(trim);
                    Array.Clear(buffer, 0, buffer.Length);

                    return resultText;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }
        private string[] GetData(NetworkStream stream, byte[] buffer)
        {
            var msg = ByteToString(stream, buffer);
            if(msg == null)
            {
                return null;
            }
            else
            {
                var temp = msg.Split(':');
                temp[temp.Length - 1] = temp[temp.Length - 1].Replace("\n", "").Replace("\r", "");
                return temp;
            }
        }
        private void SendList(NetworkStream stream)
        {
            string listResponse = "LIST";
            foreach (string key in users.Keys)
            {
                listResponse += ":" + key;
            }
            byte[] listResponseByte = new ASCIIEncoding().GetBytes(listResponse);
            stream.Write(listResponseByte, 0, listResponseByte.Length);
            listResponse = "LIST";
        }
        public override void Start()
        {
            StartListening();
            AcceptClient();
        }
    }
}
