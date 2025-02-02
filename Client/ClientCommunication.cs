using CommunicationClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public class ClientCommunication
    {
        Socket socket;
        NetworkStream stream;
        BinaryFormatter formatter;

        public ClientCommunication()
        {
            ClientController.Instance.Communication = this;
        }

        public void ConnectToServer()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("127.0.0.1", 9000);
            stream = new NetworkStream(socket);
            formatter = new BinaryFormatter();
        }

        public void DisconnectFromServer()
        {
            socket.Close();
            socket = null;
        }

        public virtual void SendRequest(Package request)
        {
            formatter.Serialize(stream, request);
        }

        public virtual Package RecieveResponse()
        {
            return (Package)formatter.Deserialize(stream);
        }
    }
}
