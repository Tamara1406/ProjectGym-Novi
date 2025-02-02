using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

namespace Server
{
    public class ServerCommunication
    {
        Socket socket;
        public void StartServer()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings["ip"]), int.Parse(ConfigurationManager.AppSettings["port"]));
            socket.Bind(ip);
            socket.Listen(10);
        }

        public void ListenToClient()
        {
            try
            {
                while (true)
                {
                    Socket client = socket.Accept();
                    ClientHandler handler = new ClientHandler(client);
                    Thread thread = new Thread(handler.HandleClient);
                    thread.Start();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void StopServer()
        {
            socket.Close();
            socket = null;
        }
    }
}
