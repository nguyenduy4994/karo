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

namespace Karo_Client
{
    public partial class frmDangNhap : Form
    {
        delegate void SetStatusDelegate(string str);
        delegate void EnableLoginDelegate(bool pEn);

        Client _client;

        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            _client = new Client();
            _client.Form = this;
            _client.Connect("localhost", 9999);
        }

        public void SetStatus(string str)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetStatusDelegate(SetStatus), str);
            }
            else
            {
                lblStatus.Text = str;
            }
        }

        public void EnableLogin(bool pEn)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EnableLoginDelegate(EnableLogin), pEn);
            }
            else
            {
                txtUsername.Enabled = txtPassword.Enabled = pEn;
                btnLogin.Enabled = btnRegister.Enabled = pEn;
            }
        }
    }
}
