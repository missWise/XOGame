using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer
{
    class ServerAPI
    {
        //Some coment
        TcpListener serverListener;
        ConnectionList connectionList;
        CommandManager commandManager;
        public ServerAPI()
        {
            connectionList = new ConnectionList();
            int port = 8888;
            serverListener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            Thread threadConnection = new Thread(new ThreadStart(ConnectionLoop));
            threadConnection.Start();
            Console.WriteLine("Server started!");
            commandManager = new CommandManager(connectionList);
        }

        private void ConnectionLoop()
        {
            serverListener.Start();
            Thread ThreadListen = new Thread(new ThreadStart(Receive));
            ThreadListen.Start();
            while (true)
            {
                var connectedClient = serverListener.AcceptTcpClient();
                StreamReader sr = new StreamReader(connectedClient.GetStream());
                string input = sr.ReadLine();
                connectionList.AddList(new Client(input, connectedClient));
            }
        }

        public void Receive()
        {
            while (true)
            {
                if (connectionList.clientList.Count == 0)
                    continue;
                for (int i = 0; i < connectionList.clientList.Count; i++)
                {
                    if (connectionList.clientList[i].user.GetStream().DataAvailable)
                    {
                        string message = connectionList.clientList[i].Read();
                        commandManager.Dispatcher(message);
                    }
                }
            }
        }
    }             
}