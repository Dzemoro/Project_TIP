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
                case "HANG":
                    return MessageType.HANG;
                default:
                    throw new ArgumentException("Type should be one of HELL, CALL, DENY, CONN, LIST, HANG");
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
                case MessageType.HANG:
                    return "HANG";
                default:
                    throw new Exception();
            }
        }

        public static UserStatus UserStatusFromString(string status)
        {
            switch (status)
            {
                case "Available":
                    return UserStatus.Available;
                case "Busy":
                    return UserStatus.Busy;
                default:
                    throw new ArgumentException("Status should be Available or Busy");
            }
        }

        public static string UserStatusToString(UserStatus status)
        {
            switch (status)
            {
                case UserStatus.Available:
                    return "Available";
                case UserStatus.Busy:
                    return "Busy";
                default:
                    throw new Exception();
            }
        }
    }
}
