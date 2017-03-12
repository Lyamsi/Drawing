using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientUDP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Clikk... ");
            Console.ReadKey();

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPAddress broadcast = IPAddress.Parse("127.0.0.1");

            byte[] sendbuf = Encoding.ASCII.GetBytes("Momomo");

            IPEndPoint ep = new IPEndPoint(broadcast, 11000);

            s.SendTo(sendbuf, ep);

            Console.WriteLine("Message sent to the broadcast address");
            Console.ReadKey();

        }
    }
}
