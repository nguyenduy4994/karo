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
using System.IO;
using System.Threading;

namespace Karo_Server
{
    public partial class frmServer : Form
    {
        delegate void SetLogDelegate(string str);

        Server gameServer;

        public frmServer()
        {
            InitializeComponent();
        }

        private void frmServer_Load(object sender, EventArgs e)
        {
            gameServer = new Server();
            gameServer.Form = this;
        }

        public void SetLog(string str)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetLogDelegate(SetLog), str);
            }
            else
            {
                txtLog.Text += str + "\r\n";
            }
        }

        private void toolClose_Click(object sender, EventArgs e)
        {
            gameServer.Close();
        }
    }
}
