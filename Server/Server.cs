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
            //ServerUDP server = new ServerUDP("127.0.0.1", 11000);
            //server.Listener();
            ServerAsync server = new ServerAsync();
            server.Start();
        }
    }
}
