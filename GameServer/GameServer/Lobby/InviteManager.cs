using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class InviteManager
    {
        public void SendInvite(Client player1, Client player2, string gameid)
        {
            try
            {
                player2.stream.Write("invite," + player1.name + "," + player2.name + "," + gameid);
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: SendInvite" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }
    }
}
