using System;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.App;


namespace XO_socket.Droid
{
    public class FragmentGame : Fragment
    {
        public static bool init = false;
        private string playerTurn;
        private string unit;
        private Button[] buttons = null;
        private TextView tvStatusBar = null;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_game, container, false);

            buttons = new Button[9];
            buttons[0] = view.FindViewById<Button>(Resource.Id.fragment_game_b1);
            buttons[1] = view.FindViewById<Button>(Resource.Id.fragment_game_b2);
            buttons[2] = view.FindViewById<Button>(Resource.Id.fragment_game_b3);
            buttons[3] = view.FindViewById<Button>(Resource.Id.fragment_game_b4);
            buttons[4] = view.FindViewById<Button>(Resource.Id.fragment_game_b5);
            buttons[5] = view.FindViewById<Button>(Resource.Id.fragment_game_b6);
            buttons[6] = view.FindViewById<Button>(Resource.Id.fragment_game_b7);
            buttons[7] = view.FindViewById<Button>(Resource.Id.fragment_game_b8);
            buttons[8] = view.FindViewById<Button>(Resource.Id.fragment_game_b9);
            Button bExit = view.FindViewById<Button>(Resource.Id.fragment_game_bExit);
            TextView tvStatusBar = view.FindViewById<TextView>(Resource.Id.fragment_game_tvStatusBar);

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Tag = i;
                buttons[i].Click += Buttons_Click;
            }
            bExit.Click += BExit_Click;

            return view;
        }

        public void BExit_Click(object sender, EventArgs e)
        {
            MyFragmentManager.Send("games,stopgame" + "," + MyFragmentManager.UserName);
            ToLobby();

        }
        public void Buttons_Click(object sender, EventArgs e)
        {
            int v = Convert.ToInt32((sender as Button).Tag);
            SendButtonClick(v);
        }

        private void SendButtonClick(int index)
        {
            MyFragmentManager.Send("games,gamexo" + "," + MyFragmentManager.UserName + "," + index);
        }

        public void MessageBox(string title, string message)
        {
            AlertDialog.Builder aDialogBuilder = new AlertDialog.Builder(MyFragmentManager.activity);
            aDialogBuilder.SetTitle(title);
            aDialogBuilder.SetMessage(message);

            aDialogBuilder.SetPositiveButton("OK", new EventHandler<DialogClickEventArgs>((sender, e) =>
            {
                ToLobby();
                CleanUp();
                (sender as Dialog).Cancel();
            }));
            MyFragmentManager.activity.StartUiThread(() =>
            {
                aDialogBuilder.SetCancelable(false);
                aDialogBuilder.Create();
                aDialogBuilder.Show();
            });
        }

        private void ToLobby()
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.main_activity_fragment_placeholder,
                    MyFragmentManager.lobbyFrag);
            transaction.Commit();
        }

        private void CleanUp()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Text = "";
                buttons[i].Clickable = true;
            }
            //tvStatusBar.setText(R.string.textview_statusbar);//проверить что выводит!!!!!
        }

        public void GameCommand(String comand)
        {
            string[] args = comand.Split(',');
            if (comand.CompareTo("yourturn") == 0)
            {
                //tvStatusBar.setText("Your turn!");
            }
            else if (comand.CompareTo("notyourturn") == 0)
            {
                //tvStatusBar.setText("notyourturn");
            }
            else if (comand.CompareTo("victory") == 0) { MessageBox("Info", "VICTORY"); }
            else if (comand.CompareTo("fail") == 0) { MessageBox("Info", "Your fail!"); }
            else if (comand.CompareTo("standoff") == 0) { MessageBox("Info", "Your standoff!"); }

            else
            //String args[] = comand.split(",");
            {
                MyFragmentManager.activity.StartUiThread(() =>
                {
                    buttons[Convert.ToInt32(args[0])].Text = args[1];
                    buttons[Convert.ToInt32(args[0])].Clickable = false;
                });
            }
        }

    }
}