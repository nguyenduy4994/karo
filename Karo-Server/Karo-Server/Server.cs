using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Karo_Server
{
    public class Server
    {
        public List<Client> Clients;
        public TcpListener Listener;

        public frmServer Form;

        public Server()
        {
            Clients = new List<Client>();
            Listener = new TcpListener(IPAddress.Any, 9999);
            Listener.Start();
            Listener.BeginAcceptTcpClient(AcceptCallback, Listener);
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            Listener.BeginAcceptTcpClient(AcceptCallback, Listener);
            Client client = new Client(Listener.EndAcceptTcpClient(ar));
            client.Form = this.Form;
            Clients.Add(client);
            Form.SetLog("Client " + client.TCPClient.Client.RemoteEndPoint.ToString() + " connected");
        }

        public void Close()
        {
            foreach (Client c in Clients)
            {
                c.WriteLine("quit ");
            }
        }
    }
}
