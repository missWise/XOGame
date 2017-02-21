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
        ClientManager cm;
        PlayersList pl;
        public MainForm()
        {         
            InitializeComponent();
            cm = new ClientManager();
            pl = new PlayersList();
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
                Thread.Sleep(1000);
                if (!(pl.lb_name.Text == "label1"))
                {
                    pl.Show();
                    this.Hide();
                }
                else MessageBox.Show("Invalid login or pass");
            }
            catch 
            {
                MessageBox.Show("Server is not available");
            }
        }


        private void btnReg_Click(object sender, EventArgs e)
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
                MessageBox.Show("Username and password could contain uppercase, lowercase letters and numbers");
                return;
            }
            try
            {
                cm.Connect("reg", tbLogin.Text, tbPassword.Text, pl);

                Thread.Sleep(1000);
                if (!(pl.lb_name.Text == "label1"))
                {
                    pl.Show();
                    this.Hide();
                }
                else MessageBox.Show("Invalid login");
            }
            catch 
            {
                MessageBox.Show("Server is not available");
            }
        }

        private bool CheckingLogin(string login, string pass)
        {
            Regex rg = new Regex("^[a-zA-Z0-9]+$");
            if(rg.IsMatch(login) && rg.IsMatch(pass))
                return true;
            return false;
        }
    }
}
