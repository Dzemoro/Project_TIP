using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;

namespace ClientClassLib
{
    public class Client
    {
        public Client() { }
        public void sendMessage(IPAddress address,String msg)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
           // IPAddress address = IPAddress.Parse("127.0.0.1");
           
            byte[] sendbuf = Encoding.ASCII.GetBytes(msg);
            IPEndPoint ep = new IPEndPoint(address, 11000);
            s.SendTo(sendbuf, ep);
            Console.WriteLine("Message sent to the address");
        }
        

    }
}
