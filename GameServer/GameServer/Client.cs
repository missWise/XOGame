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
        public UniversalStream stream;

        public Client(string name, TcpClient user,string status, UniversalStream stream)
        {
            this.name = name;
            this.user = user;
            this.status = status;
            this.stream = stream;       
        }
    }
}
