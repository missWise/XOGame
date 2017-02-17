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
            clientList.Add(client);
            Thread.Sleep(100);
            StreamWriter sw = new StreamWriter(client.user.GetStream());
            sw.WriteLine("name," + client.name);
            sw.Flush();
            Thread.Sleep(100);
            BroadcastSend();
        }

        public void Remove(Client client)
        {
            clientList.Remove(client);
            BroadcastSend();
        }

        public void Remove(string client)
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

        private void BroadcastSend()
        {
            foreach (var item in clientList)
            {              
                StreamWriter writer = new StreamWriter(item.user.GetStream());
                writer.WriteLine(MakeClientList(item.name));
                writer.Flush();
            }          
        }

        private string MakeClientList(string name)
        {
            string users = "list,";

            foreach(var item in clientList)
            {
                if (item.name == name)
                    continue;
                users += item.name + ",";
            }
            users = users.TrimEnd(',');

            return users;
        }

        public Client GetClient(string name)
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
    }
}
