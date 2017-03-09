using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XO_socket.Droid
{
    public class FragmentRA : Fragment
    {
        private string email;
        private string newUser;
        private string newPasw;

        Button bAuth;
        Button bReg;
        Button bForgotPass;

        EditText etUsername;
        EditText etPassword;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_ra, container, false);

            bAuth = view.FindViewById<Button>(Resource.Id.fragment_ra_bAuth);
            bReg = view.FindViewById<Button>(Resource.Id.fragment_ra_bReg);
            bForgotPass = view.FindViewById<Button>(Resource.Id.fragment_ra_bChangePass); //кнопка смены парол€

            etUsername = view.FindViewById<EditText>(Resource.Id.fragment_ra_etUsername);
            etPassword = view.FindViewById<EditText>(Resource.Id.fragment_ra_etPassword);

            bAuth.Click += BAuth_Click;
            bReg.Click += BReg_Click;
            bForgotPass.Click += BForgotPass_Click;

            return view;
        }

        private void BForgotPass_Click(object sender, EventArgs e)
        {
            if (etUsername != null)
            {
                MyFragmentManager.Send("sendpassword," + etUsername.Text.ToString());
                MessageBox("Info", "Your password send By Email");
            }
            else MessageBox("Error", "Put your Login");
        }

        private void BReg_Click(object sender, EventArgs e)
        {
            OpenRegistrationDialog();
        }

        private void BAuth_Click(object sender, EventArgs e)
        {
            
            MyFragmentManager.UserName = etUsername.Text.ToString();
            MyFragmentManager.SendLogin(etUsername.Text.ToString(),
                    etPassword.Text.ToString());
            ToLobby();
        }

        public void ToLobby()
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.main_activity_fragment_placeholder, MyFragmentManager.lobbyFrag);
            transaction.Commit();
        }

        private void sendReg(string mess) { MyFragmentManager.Send(mess); } //посылка регистраци на server
        private void OpenPassRetrievalDialog()
        {
            if (etUsername.Text.ToString() != null)
            {
                sendReg("sendpass," + etUsername.Text.ToString());
            }
            else
            {
                MessageBox("Error", "Incorrect Login!");
            }
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
        public void LoginRefuse()
        {
            MessageBox("Error", "Incorrect Login or password!");
        }

        public void Forgotpassrefuse() { MessageBox("Error", "Wrong Login!"); }
        public void ForgotPasswordSucces() { MessageBox("Info", "Your password was send by Email!"); }

        private void OpenRegistrationDialog()
        {
            LayoutInflater inflater = (LayoutInflater)this.Context.GetSystemService(Context.LayoutInflaterService);
            View view = inflater.Inflate(Resource.Layout.dialog_change_password, null);

            EditText username = view.FindViewById<EditText>(Resource.Id.dialog_changePass_etUsername);
            EditText pass = view.FindViewById<EditText>(Resource.Id.dialog_changePass_etPassword);
            EditText eMail = view.FindViewById<EditText>(Resource.Id.dialog_changePass_etNewPassword);

            AlertDialog.Builder aDialogBuilder = new AlertDialog.Builder(this.Context);
            aDialogBuilder.SetView(view);
            aDialogBuilder.SetMessage("Registration");

            aDialogBuilder.SetPositiveButton("Registration", new EventHandler<DialogClickEventArgs>((sender, e) =>
            {
                email = eMail.Text.ToString();
                //if (Pattern.Matches("^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$", email))
                //{
                email = eMail.Text.ToString();
                newUser = username.Text.ToString();
                newPasw = pass.Text.ToString();
                sendReg("reg," + newUser + "," + newPasw + "," + email + "," + "0");
                //}));
            }));
            aDialogBuilder.SetNegativeButton("Cancel", new EventHandler<DialogClickEventArgs>((sender, e) =>
            {
                (sender as Dialog).Cancel();
            }));

            aDialogBuilder.SetCancelable(false);//чтобы не возможно было отменить вне границ диалога
            aDialogBuilder.Create();
            aDialogBuilder.Show();
        }
    }
}
    