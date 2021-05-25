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
        HashSet<Message> messages;
        HashSet<User> users;

        public delegate void TransmissionDataDelegate(NetworkStream stream);
        public ServerAsync(string IPAddress = "127.0.0.1", int port = 8001) : base(System.Net.IPAddress.Parse(IPAddress), port)
        {
            users = new HashSet<User>();
            messages = new HashSet<Message>();
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
            string decline = "NACK";

            int listCounter = 0;
            Message msg;
            string name = "";

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
                            User u = new User(data[3], data[1], UserStatus.Available);
                            Console.WriteLine(u.Name + " connected");
                            users.Add(u);
                            name = u.Name;
                            listCounter = users.Count;
                            int portNumber = FreeTcpPort();
                            portResponse += portNumber.ToString();
                            byte[] portResponseByte = new ASCIIEncoding().GetBytes(portResponse);
                            stream.Write(portResponseByte, 0, portResponseByte.Length);
                            portResponse = "PORT:";
                            msg = new Message(null, MessageType.LIST);
                            msg.SendLIST(stream, users);
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
                            msg = new Message(data.Skip(1).ToArray(), EnumCaster.MessageTypeFromString(data[0]));
                            messages.Add(msg);
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
                            foreach(User user in users)
                            {
                                if(user.Name == data[1] || user.Name == name)
                                {
                                    user.Status = UserStatus.Busy;
                                }
                            }
                            msg = new Message(data.Skip(1).ToArray(), EnumCaster.MessageTypeFromString(data[0]));
                            messages.Add(msg);
                        }
                    }
                    else if (data[0] == "DENY")
                    {
                        if (data.Length < 3)
                        {
                            Console.WriteLine("Invalid data: DENY");
                            stream.Write(declineByte, 0, declineByte.Length);
                        }
                        else
                        {
                            msg = new Message(data.Skip(1).ToArray(), EnumCaster.MessageTypeFromString(data[0]));
                            messages.Add(msg);
                        }
                    }
                    else if(data[0] == "LIST")
                    {
                        if (data.Length < 2)
                        {
                            Console.WriteLine("Invalid data: LIST");
                            stream.Write(declineByte, 0, declineByte.Length);
                        }
                        else
                        {
                            msg = new Message(null, EnumCaster.MessageTypeFromString(data[0]));
                            msg.SendLIST(stream, users);
                        }
                    }
                    else if (data[0] == "HANG")
                    {
                        if (data.Length < 2)
                        {
                            Console.WriteLine("Invalid data: HANG");
                            stream.Write(declineByte, 0, declineByte.Length);
                        }
                        else
                        {
                            foreach(User user in users)
                            {
                                if(user.Name == data[1])
                                {
                                    user.Status = UserStatus.Available;
                                }
                            }
                        }
                    }
                    else if (data[0] == "EXIT")
                    {
                        if (data.Length < 2)
                        {
                            Console.WriteLine("Invalid data: EXIT");
                            stream.Write(declineByte, 0, declineByte.Length);
                        }
                        else
                        {
                            foreach(User user in users)
                            {
                                if(user.Name == data[1])
                                {
                                    users.Remove(user);
                                }
                            }
                        }
                    }
                }
                if (listCounter != users.Count && listCounter != 0)
                {
                    SendList(stream);
                    listCounter = users.Count;
                }
                var calls = messages.Where(x => x.MessageType == MessageType.CALL && x.Informations[0] == name);
                if (calls.Count() != 0)
                {
                    //CALL:KEY:VALUE
                    Message temp = calls.First();
                    temp.SendCALL(stream, users);
                    messages.Remove(temp);
                }
                var conns = messages.Where(x => x.MessageType == MessageType.CONN && x.Informations[0] == name);
                if (conns.Count() != 0)
                {
                    //CONN:NAME:IP:PORT
                    Message temp = conns.First();
                    temp.SendCONN(stream, users);
                    messages.Remove(temp);
                }
                var denys = messages.Where(x => x.MessageType == MessageType.DENY && x.Informations[0] == name);
                if (denys.Count() != 0)
                {
                    //DENY:NAME_TO:NAME_FROM
                    Message temp = denys.First();
                    temp.SendDENY(stream);
                    messages.Remove(temp);
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
        //Temporary Function
        private void SendList(NetworkStream stream)
        {
            string listResponse = "LIST";
            foreach (User u in users)
            {
                listResponse += ":" + u.Name;
            }
            byte[] listResponseByte = new ASCIIEncoding().GetBytes(listResponse);
            stream.Write(listResponseByte, 0, listResponseByte.Length);
        }
        public string GetUserIPAddress(string name)
        {
            foreach(User u in users)
            {
                if(u.Name == name)
                {
                    return u.IpAddress;
                }
            }
            return null;
        }
        public override void Start()
        {
            StartListening();
            AcceptClient();
        }
    }
}
