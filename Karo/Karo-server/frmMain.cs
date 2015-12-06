using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Karo_server
{
    public partial class frmMain : Form
    {

        bool isRunning = false;

        TcpListener server;

        List<Client> clients;

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (isRunning == true)
            {
                if (server != null)
                {
                    server.Stop();
                    clients.Clear();
                }

                btnStart.Text = "Start server";
                lblStatus.Text = "BẤM START ĐỂ CHẠY SERVER";
                isRunning = false;
            }
            else
            {
                btnStart.Text = "Stop";

                if (server == null)
                {
                    server = new TcpListener(IPAddress.Any, 9999);
                    clients = new List<Client>();

                    lblStatus.Text = "Server đang chạy";
                }
                
                server.Start();
                server.BeginAcceptTcpClient(AcceptCallback, server);

                isRunning = true;
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            TcpClient tcpClient = server.EndAcceptTcpClient(ar);
            server.BeginAcceptTcpClient(AcceptCallback, server);

            Client client = new Client(tcpClient);
            clients.Add(client);

            ThreadPool.QueueUserWorkItem(client.Handle);
        }
    }
}
