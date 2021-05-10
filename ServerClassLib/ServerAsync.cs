﻿using System;
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
            string calling = "CALL:";
            string confirm = "CONN:";
            int listCounter = 0;
            string callName = "";
            User u = new User();
            Message msg;

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
                            u.Name = data[3];
                            u.IPAddress = data[1];
                            Console.WriteLine(u.Name + " connected");
                            users.Add(u);
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
                            msg = new Message(data.Skip(1).ToArray(), MessageType.CALL);
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
                            msg = new Message(data.Skip(1).ToArray(), MessageType.CONN);
                            messages.Add(msg);
                        }
                    }
                }
                if (listCounter != users.Count && listCounter != 0)
                {
                    Console.WriteLine("Wysylam do: " + u.Name);
                    SendList(stream);
                    Console.WriteLine("Wyslalem");
                    listCounter = users.Count;
                }
                var calls = messages.Where(x => x.MessageType == MessageType.CALL && x.Informations[0] == u.Name);
                if (calls.Count() != 0)
                {
                    //CALL:KEY:VALUE
                    Message temp = calls.First();
                    var usernamesWithPort = temp.Informations;
                    calling += usernamesWithPort[0] + ":" + usernamesWithPort[1] + ":" + GetUserIPAddress(usernamesWithPort[1]) + ":" + usernamesWithPort[2];
                    byte[] callingByte = new ASCIIEncoding().GetBytes(calling);
                    stream.Write(callingByte, 0, callingByte.Length);
                    messages.Remove(temp);
                }
                var conns = messages.Where(x => x.MessageType == MessageType.CONN && x.Informations[0] == u.Name);
                if (conns.Count() != 0)
                {
                    //CONN:NAME:IP:PORT
                    Message temp = conns.First();
                    var usernameWithPort = temp.Informations;
                    confirm += usernameWithPort[0] + ":" + GetUserIPAddress(usernameWithPort[0]) + ":" + usernameWithPort[1];
                    byte[] confirmByte = new ASCIIEncoding().GetBytes(confirm);
                    stream.Write(confirmByte, 0, confirmByte.Length);
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
        private void SendList(NetworkStream stream)
        {
            string listResponse = "LIST";
            foreach (User u in users)
            {
                listResponse += ":" + u.Name;
            }
            byte[] listResponseByte = new ASCIIEncoding().GetBytes(listResponse);
            stream.Write(listResponseByte, 0, listResponseByte.Length);
            listResponse = "LIST";
        }
        private string GetUserIPAddress(string name)
        {
            foreach(User u in users)
            {
                if(u.Name == name)
                {
                    return u.IPAddress;
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
