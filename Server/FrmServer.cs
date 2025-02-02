using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class FrmServer : Form
    {
        ServerCommunication server;
        bool serverWorking;
        public FrmServer()
        {
            InitializeComponent();
            server = new ServerCommunication();
            server.StartServer();
            btnStopServer.Enabled = false;
            serverWorking = true;
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            if (!serverWorking)
            {
                server.StartServer();
                serverWorking = true;
                btnStartServer.Enabled = false;
                btnStopServer.Enabled = true;

            }
            Thread thread = new Thread(server.ListenToClient);
            thread.Start();
            btnStartServer.Enabled = false;
            btnStopServer.Enabled = true;
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            server.StopServer();
            serverWorking = false;
            btnStartServer.Enabled = true;
            btnStopServer.Enabled = false;
        }
    }
}
