using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClassLib
{
    public enum UserStatus
    {
        Available,
        Busy
    }
    public class User
    {
        public string Name { get; }
        public string IpAddress { get; }
        public UserStatus Status { get; set; }

        public User(string name, string ipAddress, UserStatus status)
        {
            Name = name;
            IpAddress = ipAddress;
            Status = status;
        }
    }
}
