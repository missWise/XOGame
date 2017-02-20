using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameClient
{
    public partial class GameXO : Form, IGame
    {
        NetworkStream stream;
        string name;
        public GameXO(NetworkStream stream, string name)
        {
            InitializeComponent();           
            this.stream = stream;
            this.Text = name;
            this.name = name;
            CheckForIllegalCrossThreadCalls = false;
        }

        public void ShowForm()
        {
            this.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SendInfo("6");
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            SendInfo("0");
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            SendInfo("1");
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            SendInfo("2");
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            SendInfo("3");
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            SendInfo("4");
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            SendInfo("5");
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            SendInfo("7");
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            SendInfo("8");
        }       

        public void SendInfo(string btnname)
        {
            try
            {
                StreamWriter sw = new StreamWriter(stream);
                sw.WriteLine("games,gamexo," + name + "," + btnname);
                sw.Flush();
            }
            catch
            {
                MessageBox.Show("Server is not available!");
            }
        }

        public void ReceiveGameData(string output)
        {
            string[] msg = output.Split(',');

            if (msg[1] == "victory")
            {
                MessageBox.Show(name+" won!");
               
                
                this.Close();
                return;
            }
            else if (msg[1] == "fail")
            {
                MessageBox.Show(name+" lost this game.");
                this.Close();
                return;
            }
            else if (msg[1] == "standoff")
            {
                MessageBox.Show("It's draw!");
                
                this.Close();
                return;
            }
            else if (msg[1] == "stopgame")
            {
                MessageBox.Show("The game was interrupted!");
                this.Dispose();
                return;
            }
            if (msg[1] == "yourturn")
            {
                lb_turn.Text = "Choose the cell";
                return;
            }
            if (msg[1] == "notyourturn")
            {
                lb_turn.Invoke(new Action(() => { lb_turn.Text = "Not your turn"; }));
                return;
            }

            string message = "btn" + msg[1];
            var controls = this.Controls.Find(message, true);
            var btn = controls[0] as Button;
            if (msg[1] != null)
            {
                btn.Text = msg[2];
                btn.Enabled = false;
            }
        }

        private void GameXO_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                StreamWriter sw = new StreamWriter(stream);
                sw.WriteLine("games,gamexo," + name + "," + "stopgame");
                sw.Flush();
            }
            catch { }
        }
    }
}
