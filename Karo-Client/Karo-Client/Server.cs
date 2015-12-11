using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Karo_Client
{
    public class Server
    {
        static Server _Server;
        public static Server GetInstance()
        {
            if (_Server == null)
                _Server = new Server();

            return _Server;
        }

        public TcpListener Listener;

        public Server()
        {
            Listener = new TcpListener(IPAddress.Any, 9999);
            Listener.Start();
            Listener.BeginAcceptTcpClient(AcceptCallback, Listener);
        }

        private void AcceptCallback(IAsyncResult ar)
        {
 	        
        }
    }
}
