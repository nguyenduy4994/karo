﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace Karo_Client
{
    public class Client
    {
        static Client Instance;
        public static Client GetInstance()
        {
            if (Instance == null)
                Instance = new Client();
            return Instance;
        }

        public TcpClient TCPClient;
        NetworkStream _NS;
        StreamReader _SR;
        StreamWriter _SW;

        public frmDangNhap Form;

        public Client()
        {
            TCPClient = new TcpClient();
        }

        public void Connect(string host, int port)
        {
            TCPClient.BeginConnect(host, port, RequestCallback, TCPClient);
        }

        private void RequestCallback(IAsyncResult ar)
        {
            try
            {
                TCPClient.EndConnect(ar);

                if (TCPClient.Connected)
                {
                    _NS = TCPClient.GetStream();
                    _SW = new StreamWriter(_NS);
                    _SR = new StreamReader(_NS);

                    Form.SetStatus("Connected to server");
                    Form.EnableLogin(true);

                    ThreadPool.QueueUserWorkItem(Handle);
                }
            }
            catch (Exception ex)
            {
                Form.SetStatus(ex.Message);
                Form.EnableLogin(false);
            }
        }

        public void WriteLine(string str)
        {
            _SW.WriteLine(str);
            _SW.Flush();
        }

        public void Handle(object obj)
        {
            try
            {
                while (true)
                {
                    string line = _SR.ReadLine();
                    string command = line.Substring(0, line.IndexOf(' ')).ToLower();
                    string args = line.Substring(command.Length + 1);
                    string respone = string.Empty;

                    switch (command)
                    {
                        case "quit":
                            CommandQuit();
                            break;
                        case "password_please":
                            respone = CommandPassword();
                            break;
                        case "logged":
                            CommandLogged();
                            break;
                    }

                    if (command == "quit") return;

                    if(respone != string.Empty)
                        WriteLine(respone);
                }
            }
            catch (Exception ex)
            {
                Form.SetStatus(ex.Message);
                Form.EnableLogin(false);
            }
        }

        private void CommandLogged()
        {
            MessageBox.Show("dang nhap thanh cong");
        }

        private string CommandPassword()
        {
            return "pass " + Form.txtPassword.Text;
        }

        private void CommandQuit()
        {
            _NS.Close();
            _SR.Close();
            _SW.Close();
            TCPClient.Close();
            Form.SetStatus("Disconnected");
            Form.EnableLogin(false);
        }

        public void CommandLogin()
        {
            WriteLine("login " + Form.txtUsername.Text);
        }
    }
}
