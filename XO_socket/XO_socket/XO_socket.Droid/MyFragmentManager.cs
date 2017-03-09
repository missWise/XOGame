

using Android.App;

using WebSocket4Net;

namespace XO_socket.Droid
{
    public class MyFragmentManager
    {
        public static WebSocket ws;
        public static FragmentLobby lobbyFrag;
        public static FragmentRA regFrag;
        public static MainActivity activity;
        public static FragmentGame gameFrag;
        public static string UserName = null;
        public static string SessionKey = null;//must be "xo"

        static MyFragmentManager()
        {
            lobbyFrag = new FragmentLobby();
            regFrag = new FragmentRA();
            gameFrag = new FragmentGame();
        }
        public static void ShowGame(FragmentTransaction transaction)
        {
            transaction.Replace(Resource.Id.main_activity_fragment_placeholder,
                    MyFragmentManager.gameFrag);
            transaction.Commit();
        }
        public static void InitWebSocket(WebSocket webSocket)
        {
            ws = webSocket;
        }
        public static void Send(string message)
        {
            ws.Send(message);
        }
        public static void SendLogin(string name, string pass)
        {
            Send("auth," + name + "," + pass + ",0");
        }
        public static void SendInviteRequest(string name)
        {
            Send("lobby,invite," + UserName + "," + name + ",XO");
        }
    }
}