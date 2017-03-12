using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WhiteBoardServer

{

    public class CClient

    {      
        public static List<CClient> listClient = new List<CClient>();

        private ManualResetEvent m_StopEvent;

        private Thread m_Thread;



        private TcpClient m_TcpClient;

        private NetworkStream m_NetworkStream;

        private byte[] bytesFrom = new byte[2048];

        public TcpClient TcpClient
        {
            get { return m_TcpClient; }
        }


        public CClient(TcpClient MyTcpClient)
        {

            m_StopEvent = new ManualResetEvent(false);

            m_Thread = new Thread(Manage);

            m_Thread.Name = "CClient " + MyTcpClient.ToString();

            m_TcpClient = MyTcpClient;

            m_NetworkStream = m_TcpClient.GetStream();

        }



        private void SendData()
        {

                foreach (CClient cl in listClient)
                {
                // TODO ne pas envoyer à lui même
                try
                {
                    cl.TcpClient.GetStream().Write(bytesFrom, 0, bytesFrom.Length);

                    cl.TcpClient.GetStream().Flush();// clear buffer
                }
                catch (Exception)
                {
                    Console.WriteLine("Lost " + cl.m_Thread.Name);
                }

        }
          

            Stop();// passe la main
        }



        public void Start()

        {
            Console.WriteLine(" >> Fonction  Started");
            m_StopEvent.Reset();// blocage des threads
            m_Thread.Start();

        }



        private void Stop()

        {
            Console.WriteLine(" >> Fonction Stop");
            m_StopEvent.Set();// déblocage des threads

        }



        private void Manage()

        {
            Console.WriteLine(" >> Server Manage");
            //while (m_StopEvent.WaitOne(10000, false))//Bloque le thread jusqu'à ce que l'instance actuelle reçoive un signal

            {
               // Start();
                Console.WriteLine(" >> Client" + m_NetworkStream);

                m_NetworkStream.Read(bytesFrom, 0, 2048);

                SendData();
            }

        }

    }

}
