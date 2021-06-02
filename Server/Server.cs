using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerClassLib;

namespace Server
{
    class Server
    {
        static void Main(string[] args)
        {
            ServerAsync server = new ServerAsync("192.168.0.190");
            server.Start();
        }
    }
}
