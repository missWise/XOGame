using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class Lobby
    {
        InviteManager inviteManager;
        CommandManager commandManager;
        public Lobby(CommandManager commandManager)
        {
            inviteManager = new InviteManager();
            this.commandManager = commandManager;
        }
        public void SetCommand(string command)
        {
            command = command.Replace("\r\n", "");
            string[] msg = command.Split(',');
            switch(msg[1])
            {
                case "invite":
                    if(commandManager.connectionList.GetClient(msg[2]).status != "1")
                    inviteManager.SendInvite(commandManager.connectionList.GetClient(msg[2]), commandManager.connectionList.GetClient(msg[3]), msg[4]);
                    break;
                case "exit":
                    commandManager.connectionList.Remove(commandManager.connectionList.GetClient(msg[2]));
                    break;
                case "changepass":
                    DataBaseManager db = new DataBaseManager();
                    db.ChangePassword(msg[2], msg[3]);
                    break;
            }
        }
    }
}
