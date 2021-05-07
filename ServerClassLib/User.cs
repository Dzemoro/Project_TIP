using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClassLib
{
    public class User
    {
        string name;
        string ipAddress;

        public User()
        {
            this.name = "";
            this.ipAddress = "";
        }

        public string Name
        {
            get => name;
            set => name = value;
        }
        public string IPAddress
        {
            get => ipAddress;
            set => ipAddress = value;
        }
    }
}
