using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ServerClassLib
{
    public enum MessageType
    {
        HELL,
        CALL,
        DENY,
        CONN,
        LIST,
        HANG
    }
    public class Message
    {
        public string[] Informations { get; set; }
        public MessageType MessageType { get; set; }

        public Message(string[] informations, MessageType messageType)
        {
            this.Informations = informations;
            this.MessageType = messageType;
        }

        public void SendCALL(NetworkStream stream, HashSet<User> users)
        {
            string call = EnumCaster.MessageTypeToString(MessageType) + ":" + Informations[0] + ":" + Informations[1] + ":" + GetUserIPAddress(Informations[1], users) + ":" + Informations[2];
            Send(stream, call);
        }

        public void SendDENY(NetworkStream stream)
        {
            string deny = EnumCaster.MessageTypeToString(MessageType) + ":" + Informations[0] + ":" + Informations[1];
            Send(stream, deny);
        }

        public void SendCONN(NetworkStream stream, HashSet<User> users)
        {
            string confirm = EnumCaster.MessageTypeToString(MessageType) + ":" + Informations[0] + ":" + GetUserIPAddress(Informations[0], users) + ":" + Informations[1];
            Send(stream, confirm);
        }

        public void SendLIST(NetworkStream stream, HashSet<User> users)
        {
            string listResponse = EnumCaster.MessageTypeToString(MessageType);
            foreach (User u in users)
            {
                if(u.Status == UserStatus.Available)
                {
                    listResponse += ":" + u.Name;
                }
            }
            Send(stream, listResponse);
        }

        public void SendHANG(NetworkStream stream)
        {
            string hang = EnumCaster.MessageTypeToString(MessageType);
            Send(stream, hang);
        }

        public string GetUserIPAddress(string name, HashSet<User> users)
        {
            foreach (User u in users)
            {
                if (u.Name == name)
                {
                    return u.IpAddress;
                }
            }
            return null;
        }

        public void Send(NetworkStream stream, string msg)
        {
            byte[] msgByte = new ASCIIEncoding().GetBytes(msg);
            stream.Write(msgByte, 0, msgByte.Length);
        }
    }

}
