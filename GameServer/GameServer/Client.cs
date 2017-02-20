using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class Client
    {
        public string name;
        public TcpClient user;
        public string status;

        public Client(string name, TcpClient user,string status)
        {
            this.name = name;
            this.user = user;
            this.status = status;           
        }
        public string Read()
        {
            StreamReader sr = new StreamReader(user.GetStream());
            return sr.ReadLine();
        }
    }
}
