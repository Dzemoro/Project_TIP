using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClassLib
{
    public class EnumCaster
    {
        public static MessageType MessageTypeFromString(string type)
        {
            switch (type)
            {
                case "HELL":
                    return MessageType.HELL;
                case "CALL":
                    return MessageType.CALL;
                case "DENY":
                    return MessageType.DENY;
                case "CONN":
                    return MessageType.CONN;
                case "LIST":
                    return MessageType.LIST;
                default:
                    throw new ArgumentException("Type should be one of HELL, CALL, DENY, CONN");
            }
        }

        public static string MessageTypeToString(MessageType type)
        {
            switch (type)
            {
                case MessageType.HELL:
                    return "HELL";
                case MessageType.CALL:
                    return "CALL";
                case MessageType.DENY:
                    return "DENY";
                case MessageType.CONN:
                    return "CONN";
                case MessageType.LIST:
                    return "LIST";
                default:
                    throw new Exception();
            }
        }
    }
}
