using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClassLib
{
    public enum MessageType
    {
        HELL,
        CALL,
        DENY,
        CONN
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
    }

}
