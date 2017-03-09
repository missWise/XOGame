using System;
using System.Collections.Generic;


using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace XO_socket.Droid
{
    public class FragmentLobby : Fragment
    {
        Button LogIn = null;
        Button bLogout = null;
        Button bRefresf = null;
        Button bChangePSW = null;
        TextView tvName = null;
        TextView tvStatus = null;
        ListView lvFreePlayersList = null;
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_lobby, container, false);
            
            bLogout = view.FindViewById<Button>(Resource.Id.fragment_lobby_bLogout);
            bRefresf = view.FindViewById<Button>(Resource.Id.fragment_lobby_bInvite);
            bChangePSW = view.FindViewById<Button>(Resource.Id.fragment_lobby_bChange_PSW);
            tvName = view.FindViewById<TextView>(Resource.Id.fragment_lobby_tvStatusBar_name);
            tvStatus = view.FindViewById<TextView>(Resource.Id.fragment_lobby_tvStatusBar);
            LogIn = view.FindViewById<Button>(Resource.Id.fragment_lobby_bLogin);

            lvFreePlayersList = view.FindViewById<ListView>(Resource.Id.fragment_lobby_lvFreePlayersList);

            LogIn.Click += BLogin_Click;
            bLogout.Click += BLogout_Click;
            bRefresf.Click += BRefresf_Click;
            bChangePSW.Click += BChangePSW_Click;


        return view;
    }

        private void BLogin_Click(object sender, EventArgs e)
        {
            MyFragmentManager.activity.InitWebSocket();
            MyFragmentManager.InitWebSocket(MyFragmentManager.activity.webSocket);
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.main_activity_fragment_placeholder,
                new FragmentRA());
            transaction.Commit();
        }

        private void BLogout_Click(object sender, EventArgs e)
        {
            ToReg();
            MyFragmentManager.Send("lobby,exit" + "," + MyFragmentManager.UserName);
        }


        private void BRefresf_Click(object sender, EventArgs e)
        {
            MyFragmentManager.Send("list" + "," + "");
        }

        private void BChangePSW_Click(object sender, EventArgs e)
        {
            ChangePSW();
        }

        public void AddToPlayerList(string[] names)
        {
            MyFragmentManager.activity.StartUiThread(() =>
            {
                bLogout.Visibility = ViewStates.Visible;
                bRefresf.Visibility = ViewStates.Visible;
                bChangePSW.Visibility = ViewStates.Visible;
                tvName.Text = "Your name: " + MyFragmentManager.UserName;
            });
            List<string> nameList = new List<string>();
            for (int i = 1; i < names.Length; i++)
            {
                if (!names[i].Equals(MyFragmentManager.UserName))
                {
                    string[] tmp = names[i].Split('#');
                    if (tmp[1].CompareTo("0") == 0)
                    { nameList.Add(tmp[0]); }
                }
            }
            MyFragmentManager.activity.StartUiThread(() =>
            {
                AdapterXO adapter = new AdapterXO(MyFragmentManager.activity, nameList);
                lvFreePlayersList.Adapter = adapter;
            });
        }
        public void Autherization(bool state)
        {
            if (state)
            {
                SetStatusBarData(MyFragmentManager.UserName, "Connected to the server");
            }
            else
            {
                MyFragmentManager.UserName = "";
                SetStatusBarData(MyFragmentManager.UserName, "Waiting for connection to the server...");
            }
        }

        public void Registation(bool state)
        {
            if (state)
            {
                SetStatusBarData(MyFragmentManager.UserName, "Connected to the server");
            }
            else
            {
                MyFragmentManager.UserName = "";
                SetStatusBarData(MyFragmentManager.UserName, "Waiting for connection to the server...");
            }
        }

        private void SetStatusBarData(string name, string status)
        {
            tvName.Text = "Your name: " + name;
            tvStatus.Text = status;
        }

        public void MessageBox(string title, string message)
        {
            AlertDialog.Builder aDialogBuilder = new AlertDialog.Builder(MyFragmentManager.activity);
            aDialogBuilder.SetTitle(title);
            aDialogBuilder.SetMessage(message);

            aDialogBuilder.SetPositiveButton("OK", new EventHandler<DialogClickEventArgs>((sender, e) =>
            {
                (sender as Dialog).Cancel();
            }));

            MyFragmentManager.activity.StartUiThread(() => 
            {
                aDialogBuilder.SetCancelable(false);
                aDialogBuilder.Create();
                aDialogBuilder.Show();
            });
        }
        private void ChangePSW()
        {
            LayoutInflater inflater = (LayoutInflater)this.Context.GetSystemService(Context.LayoutInflaterService);
            View v = inflater.Inflate(Resource.Layout.newpsw, null);

            EditText dialog1 = v.FindViewById<EditText>(Resource.Id.new_PSW);
            AlertDialog.Builder aDialogBuilder = new AlertDialog.Builder(this.Context);
            aDialogBuilder.SetView(v);
            aDialogBuilder.SetPositiveButton("Ok", new EventHandler<DialogClickEventArgs>((sender, e) =>
            {
                MyFragmentManager.Send("lobby,changepass," + MyFragmentManager.UserName + "," + dialog1.Text.ToString());
                (sender as Dialog).Cancel();
            }));
            aDialogBuilder.SetCancelable(false);
            aDialogBuilder.Create();
            aDialogBuilder.Show();
        }

    public void InvitationBox(string playerName)
    {
        AlertDialog.Builder aDialogBuilder = new AlertDialog.Builder(this.Context);
        aDialogBuilder.SetMessage("Invitation");
        aDialogBuilder.SetMessage("Player " + playerName + " wants to play with you!");

            aDialogBuilder.SetPositiveButton("OK", new EventHandler<DialogClickEventArgs>((sender, e) =>
            {
                MyFragmentManager.Send("games,ask,yes," + playerName + "," + MyFragmentManager.UserName + ",XO");
                (sender as Dialog).Cancel();
            }));

            aDialogBuilder.SetNegativeButton("Cancel", new EventHandler<DialogClickEventArgs>((sender, e) =>
            {
                MyFragmentManager.Send("games,ask,no," + playerName + "," + MyFragmentManager.UserName + ",XO");
                (sender as Dialog).Cancel();
            }));

            MyFragmentManager.activity.StartUiThread(() =>
            {
                aDialogBuilder.SetCancelable(false);
                aDialogBuilder.Create();
                aDialogBuilder.Show();
            });
        }

       public void ToReg()
       {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.main_activity_fragment_placeholder, MyFragmentManager.regFrag);
            transaction.Commit();
       }
    }
}