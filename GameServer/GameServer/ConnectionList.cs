using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer
{
    class ConnectionList
    {
        public  List<Client> clientList;   

        public ConnectionList()
        {
            clientList = new List<Client>();            
        }

        public void AddList(Client client)
        {
            try
            {
                //throw new ArgumentException();
                clientList.Add(client);
                Thread.Sleep(100);
                client.stream.Write("loginsuccess," + client.name);
                Thread.Sleep(100);
                BroadcastSend();
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: AddList" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }

        public void Remove(Client client)
        {
            try
            {
                clientList.Remove(client);
                BroadcastSend();
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: Remove" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }

        public void Remove(string client)
        {
            try
            {
                foreach (var item in clientList)
                {
                    if (item.name == client)
                    {
                        clientList.Remove(item);
                    }
                }
                BroadcastSend();
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: Remove" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }

        public void BroadcastSend()
        {
            try
            {
                foreach (var item in clientList)
                {
                    item.stream.Write(MakeClientList(item.name));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: BroadcastSend" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }

        private string MakeClientList(string name)
        {
            try
            {
                string users = "list,";

                foreach (var item in clientList)
                {
                    if (item.name == name)
                        continue;
                    users += item.name + "#" + item.status + ",";
                }
                users = users.TrimEnd(',');

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: MakeClientList" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }

        public Client GetClient(string name)
        {
            try
            {
                Client client = null;
                foreach (var item in clientList)
                {
                    if (item.name == name)
                    {
                        client = item;
                        break;
                    }
                }
                return client;
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: GetClient" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }
    }
}
