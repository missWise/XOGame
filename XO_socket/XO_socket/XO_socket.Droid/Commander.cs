

namespace XO_socket.Droid
{
    public class Commander
    {
        public static void listenLoop(string message)
        {
            //String command = message.substring(0 ,message.indexOf(","));
            message = message.Replace("\r\n", "");
            string mess = message.Substring(message.IndexOf(",") + 1);
            string[] args = message.Split(',');

            switch (args[0])
            {
                case "loginsuccess":
                    {
                        MyFragmentManager.lobbyFrag.AddToPlayerList(args);
                        break;
                    }
                case "list":
                    {
                        MyFragmentManager.lobbyFrag.AddToPlayerList(args);
                        break;
                    }
                case "loginrefuse":
                    {
                        MyFragmentManager.regFrag.LoginRefuse(); //MsgIncorrect login or pass
                        break;
                    }
                case "forgotpasssuccess":
                    {
                        MyFragmentManager.regFrag.ForgotPasswordSucces();//Msg Your password was send to your email!
                        break;
                    }
                case "forgotpassrefuse":
                    {
                        MyFragmentManager.regFrag.Forgotpassrefuse(); //Msg Invalid Login!
                        break;
                    }
                case "invite":
                    {
                        MyFragmentManager.lobbyFrag.InvitationBox(args[1]);
                        break;
                    }
                case "ask":
                    {
                        if (Equals(args[1], "XO"))
                        {
                            MyFragmentManager.SessionKey = "yes";
                            MyFragmentManager.ShowGame(MyFragmentManager.lobbyFrag.FragmentManager.BeginTransaction());
                        }
                        else
                        {
                            MyFragmentManager.lobbyFrag.MessageBox("Connection denied", "Invitation issue");
                        }
                        break;
                    }
                case "gamexo":
                    {
                        MyFragmentManager.gameFrag.GameCommand(mess);
                        break;
                    }
            }
        }
    }
}