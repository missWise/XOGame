using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer
{
    class ServerAPI
    {
        TcpListener serverListener;
        ConnectionList connectionList;
        CommandManager commandManager;
        DataBaseManager dbmanager;
        public ServerAPI()
        {
            connectionList = new ConnectionList();
            int port = 8888;
            serverListener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            Thread threadConnection = new Thread(new ThreadStart(ConnectionLoop));
            threadConnection.Start();
            Console.WriteLine("Server started!");
            commandManager = new CommandManager(connectionList);
            dbmanager = new DataBaseManager();
        }

        private void ConnectionLoop()
        {
            serverListener.Start();
            LogManager.AddToLog("server", "started");
            Thread ThreadListen = new Thread(new ThreadStart(Receive));
            ThreadListen.Start();
            while (true)
            {
                try
                {
                    var connectedClient = serverListener.AcceptTcpClient();
                    UniversalStream stream = new UniversalStream(connectedClient);

                    while (!connectedClient.GetStream().DataAvailable) { }

                    string data = stream.Read();
                    if (new Regex("^GET").IsMatch(data))
                    {
                        stream.WriteHandshake(data);
                        stream.Type = UniversalStream.ClientType.Web;
                        while (!connectedClient.GetStream().DataAvailable) { }

                        data = stream.Read();
                    }

                    if (stream.Type == UniversalStream.ClientType.Web)
                        data = stream.Decode();

                    data = data.Replace("\r\n", "");
                    string[] input = data.Split(',');

                    if (input[0] == "sendpassword")
                    {
                        if (dbmanager.SendPassword(input[1]))
                        {
                            stream.Write("forgotpasssuccess");
                        }
                        else
                        {
                            stream.Write("forgotpassrefuse");
                        }
                    }

                    if (input[0] == "foreign")
                    {
                        Client cl = connectionList.clientList.Find(c => c.name == input[1]);
                        if (cl == null)
                            connectionList.AddList(new Client(input[1], connectedClient, input[2], stream));
                        LogManager.AddToLog(input[1], data);
                    }

                    else if (dbmanager.RA(input[0], input[1], input[2], input[3]))
                    {
                        Client cl = connectionList.clientList.Find(c => c.name == input[1]);
                        if (cl == null)
                            connectionList.AddList(new Client(input[1], connectedClient, input[3], stream));
                        LogManager.AddToLog(input[1], data);

                    }
                    else
                    {
                        stream.Write("loginrefuse");
                        connectedClient.Close();
                        
                    }
                    Thread.Sleep(50);
                }
                catch (Exception ex)
                {
                    CrashManager.CrashReportToFile(ex.StackTrace + ex.Message, ex.InnerException?.ToString());
                }
            }
        }

        public void Receive()
        {
            try
            {
                while (true)
                {
                    if (connectionList.clientList.Count == 0)
                        continue;
                    for (int i = 0; i < connectionList.clientList.Count; i++)
                    {
                        if (connectionList.clientList[i].user.GetStream().DataAvailable)
                        {
                            string message = connectionList.clientList[i].stream.Read();
                            LogManager.AddToLog(connectionList.clientList[i].name, message);

                            if (connectionList.clientList[i].stream.Type == UniversalStream.ClientType.Web)
                                message = connectionList.clientList[i].stream.Decode();

                            commandManager.Dispatcher(message);
                        }
                    }
                    Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {
                CrashManager.CrashReportToFile(ex.StackTrace + ex.Message, ex.InnerException?.ToString());
            }
        }
    }
}