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

        string Username;
        string Password;

        public Client(TcpClient pClient)
        {
            TCPClient = pClient;
            ns = pClient.GetStream();
            sr = new StreamReader(ns);
            sw = new StreamWriter(ns);
            sw.AutoFlush = true;
        }

        public void WriteLine(string str)
        {
            sw.WriteLine(str);
        }
        
        public void HandleRequest(object obj)
        {
            try
            {
                while (true)
                {
                    string line = sr.ReadLine();
                    string command = line.Substring(0, line.IndexOf(' ')).ToLower();
                    string args = line.Substring(command.Length + 1).Trim();
                    string respone = string.Empty;

                    switch (command)
                    {
                        case "login":
                            respone = CommandLogin(args);
                            break;
                        case "quit":
                            CommandQuit();
                            break;
                        case "pass":
                            respone = CommandPass(args);
                            break;
                    }

                    if (command == "quit") return;

                    if (respone != string.Empty)
                    {
                        WriteLine(respone);
                    }
                }
            }
            catch (Exception ex)
            {
                Form.SetLog(TCPClient.Client.RemoteEndPoint.ToString() + ": " + ex.Message);
            }
        }

        private string CommandPass(string args)
        {
            Password = args;
            Form.SetLog(TCPClient.Client.RemoteEndPoint.ToString() + ": username=" + Username + ", password=" + Password);
            return "logged ";
        }

        private void CommandQuit()
        {
            ns.Close();
            sr.Close();
            sw.Close();
            TCPClient.Close();
        }

        private string CommandLogin(string args)
        {
            Username = args;
            return "password_please ";
        }
    }
}
