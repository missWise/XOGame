using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameClient
{
    public partial class PasswordForm : Form
    {
        public NetworkStream stream;
        public string name;
        public PasswordForm(NetworkStream stream, string name)
        {
            InitializeComponent();
            this.stream = stream;
            this.name = name;
        }

        private void btnPass_Click(object sender, EventArgs e)
        {
            if (tbPass.Text == "")
            {
                MessageBox.Show("Enter new password");
                return;

            }
            else if (!CheckingPass(tbPass.Text))
            {
                MessageBox.Show("Invalid password!");
                return;
            }
            else
            {
                StreamWriter sw = new StreamWriter(stream);
                sw.WriteLine("lobby,changepass," + name + "," + tbPass.Text);
                sw.Flush();
                Hide();
            }
        }
        private bool CheckingPass(string pass)
        {
            Regex rg = new Regex("^[a-zA-Z0-9]+$");
            if (rg.IsMatch(pass))
                return true;
            return false;
        }

    }
}
