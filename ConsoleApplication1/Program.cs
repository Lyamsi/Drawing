using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

/*

*

 * sdisrv-tfs01.sipleo.com

*/

namespace WhiteBoardServer

{

    class Program

    {
        public static int Main()
        {
            Console.WriteLine("Appui pr Start listener...");
            Console.ReadKey();
            StartListener();
            return 0;
        }
        private const int listenPort = 11000;

        private static void StartListener()
        {
            bool done = false;

            UdpClient listener = new UdpClient(listenPort);

            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

            try
            {
                while (!done)
                {
                    Console.WriteLine("Waiting for broadcast");
                    byte[] bytes = listener.Receive(ref groupEP);

                    Console.WriteLine("Received broadcast from {0} :\n {1}\n",
                        groupEP.ToString(),
                        Encoding.ASCII.GetString(bytes, 0, bytes.Length));
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                listener.Close();
            }
        }


    }

}
