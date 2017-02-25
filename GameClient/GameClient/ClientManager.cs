﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameClient
{
    public class ClientManager
    {
        TcpClient client;
        public NetworkStream netStream;
        PlayersList playersList;
        IGame game;
        MainForm mf;
        Thread receiveThread;
        public ClientManager(MainForm mf)
        {
            this.mf = mf;
            receiveThread = new Thread(new ThreadStart(ReceiveData));
        }
       
        public ClientManager(){}

        public void Connect(string command, string name, string password, PlayersList pl)
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);   
            client = new TcpClient();
            client.Connect(ipe);
            netStream = client.GetStream();
           
            SendLogin(command, name, password);
           
            this.playersList = pl;
            playersList.name = name;
            playersList.stream = netStream;

            if (!receiveThread.IsAlive)
            {
                receiveThread.Start();
            }
        }

        public void Send(string mesage)
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
            client = new TcpClient();
            client.Connect(ipe);
            netStream = client.GetStream();
            StreamWriter sw = new StreamWriter(netStream);
            sw.WriteLine(mesage);
            sw.Flush();
            receiveThread.Start();
        }

        void SendLogin(string command, string name, string password)
        {
            try
            {
                StreamWriter sw = new StreamWriter(netStream);
                sw.WriteLine(command + "," + name + "," + password + "," + "0");
                sw.Flush();
            }
            catch
            {
                MessageBox.Show("Server is not available!");
            }
        }
       
        void ReceiveData()
        {
            while (true)
            {
                string output = "";

                if (netStream.DataAvailable)
                {
                    StreamReader sr = new StreamReader(netStream);
                    output = sr.ReadLine();
                    string[] msg = output.Split(',');
                    switch (msg[0])
                    {
                        case "loginsuccess":
                            playersList.lb_name.Text = msg[1];
                            Thread thread = new Thread(new ThreadStart(mf.pl.ShowForm));
                            mf.Hide();
                            mf.rf.Hide();
                            thread.Start();
                            break;
                        case "loginrefuse":
                            MessageBox.Show("Incorrect login or pass");
                            break;
                        case "forgotpasssuccess":
                            MessageBox.Show("Your password was sent to your email!");
                            break;
                        case "forgotpassrefuse":
                            MessageBox.Show("Invalid login!");
                            break;
                        case "list":
                            playersList.AddList(msg);
                            break;
                        case "invite":
                            AskRequest ar = new AskRequest(msg[1], msg[2], msg[3], netStream, playersList);
                            Thread tr = new Thread(new ThreadStart(ar.ShowForm));
                            tr.Start();
                            break;
                        case "ask":
                            if (msg[1] == "XO")
                            {
                                game = new GameXO(netStream, playersList.name);
                                Thread tr1 = new Thread(new ThreadStart(game.ShowForm));
                                tr1.Start();
                            }
                            break;
                        case "gamexo":
                            game.ReceiveGameData(output);
                            break;
                    }
                }
                Thread.Sleep(100);             
            }
        }
    }
}
