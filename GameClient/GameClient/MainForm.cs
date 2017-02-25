using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameClient
{
    public partial class MainForm : Form
    {
        public ClientManager cm;
        public PlayersList pl;
        public RegistrationForm rf;
        public MainForm()
        {         
            InitializeComponent();
            cm = new ClientManager(this);
            pl = new PlayersList();
            rf = new RegistrationForm(this);
            CheckForIllegalCrossThreadCalls = false;
        }
        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbLogin.Text == "" || tbPassword.Text == "")
            {
                MessageBox.Show("Empty username or password!");
                return;
            }
            else if (tbLogin.Text.Length > 15)
            {
                MessageBox.Show("Very long username! Enter username till 15 symbols.");
                return;
            }
            else if (!CheckingLogin(tbLogin.Text, tbPassword.Text))
            {
                MessageBox.Show("Invalid password or username!");
                return;
            }
            try
            {
                cm.Connect("auth", tbLogin.Text, tbPassword.Text, pl);
            }
            catch 
            {
                MessageBox.Show("Server is not available");
            }
        }


        private void btnReg_Click(object sender, EventArgs e)
        {
            rf.Show();
            Hide();
        }

        private bool CheckingLogin(string login, string pass)
        {
            Regex rg = new Regex("^[a-zA-Z0-9]+$");
            if(rg.IsMatch(login) && rg.IsMatch(pass))
                return true;
            return false;
        }

        private void btnForgotPass_Click(object sender, EventArgs e)
        {
            if (tbLogin.Text == "")
            {
                MessageBox.Show("Please, enter login!");
                return;
            }
            try
            {
                cm.Send("sendpassword," + tbLogin.Text);
                return;
            }
            catch
            {
                MessageBox.Show("Server is not available");
            }
        }
    }
}
