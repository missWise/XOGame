using System;
using Android.App;
using Android.OS;
using WebSocket4Net;

namespace XO_socket.Droid
{
    [Activity(Label = "XO_socket.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public WebSocket webSocket;

        public void StartUiThread(Action act)
        {
            RunOnUiThread(act);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            MyFragmentManager.activity = this;
            SetContentView(Resource.Layout.Main);

            // InitWebSocket();
            //MyFragmentManager.InitWebSocket(webSocket);


            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.main_activity_fragment_placeholder,
                    MyFragmentManager.lobbyFrag);
            transaction.Commit();
        }
        public void InitWebSocket()
        {
            webSocket = new WebSocket("ws://10.0.3.2:8888/");
            webSocket.Opened += OnConnectionOpen;
            webSocket.MessageReceived += OnMessage;
            webSocket.Open();
        }
        private void OnMessage(object sender, MessageReceivedEventArgs e)
        {
            Commander.listenLoop(e.Message);
        }

        private void OnConnectionOpen(object sender, EventArgs e)
        {
        }
    }
}


