using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Karo_server
{
    public class Client
    {
        public TcpClient tcpClient;
        public NetworkStream ns;
        public StreamReader sr;
        public StreamWriter sw;

        public Client(TcpClient pClient)
        {
            tcpClient = pClient;

            ns = pClient.GetStream();
            sr = new StreamReader(ns);
            sw = new StreamWriter(ns);

            sw.WriteLine("200 Connected to server");
            sw.Flush();
        }

        public void Handle(object obj)
        {

        }
    }
}
