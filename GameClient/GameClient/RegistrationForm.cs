using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameClient
{
    public partial class RegistrationForm : Form
    {
        MainForm mf;
        public RegistrationForm(MainForm mf)
        {
            InitializeComponent();
            this.mf = mf;
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
                mf.cm.Connect("reg", tbLogin.Text, tbPassword.Text, mf.pl);
            }
            catch
            {
                MessageBox.Show("Server is not available");
            }
        }

        private bool CheckingLogin(string login, string pass)
        {
            Regex rg = new Regex("^[a-zA-Z0-9]+$");
            if (rg.IsMatch(login) && rg.IsMatch(pass))
                return true;
            return false;
        }
        private bool CheckingEmail(string email)
        {
            Regex rg = new Regex(@" ^ (? ("")(""[^ ""] +? ""@) | (([0 - 9a - z]((\.(? !\.)) |[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$");
            if (rg.IsMatch(email))
                return true;
            return false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Dispose();
            mf.Show();
        }
    }
}
