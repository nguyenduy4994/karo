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
    public class Client
    {
        public TcpClient TCPClient;
        NetworkStream ns;
        StreamReader sr;
        StreamWriter sw;

        public frmServer Form;

        public Client(TcpClient pClient)
        {
            TCPClient = pClient;
            ns = pClient.GetStream();
            sr = new StreamReader(ns);
            sw = new StreamWriter(ns);
        }

        public void WriteLine(string str)
        {
            sw.WriteLine(str);
            sw.Flush();
        }

        public void HandleRequest(object obj)
        {

        }
    }
}
