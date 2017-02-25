using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class DataBaseManager
    {
        string databaseName = @"D:\XODatabase.db";
        string tablename = "PersonalData";
        SQLiteConnection connection;
        public DataBaseManager() { }
        public void Conn()
        {
            connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));
            connection.Open();
        }
        public void Insert(string login, string password, string email)
        {
            Conn();
            SQLiteCommand command = new SQLiteCommand("INSERT INTO '" + tablename + "' ('login', 'password', 'email') VALUES ('" + login + "','" + password + "','" + email + "');", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public bool RA(string command, string login, string password, string email)
        {
            bool result = false;
            switch (command)
            {
                case "reg":
                    if (!CheckingIfExist(login))
                    {
                        Insert(login, password, email);
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    break;

                case "auth":
                    if (CheckingPassword(login, password))
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    break;
            }
            return result;
        }
        
        public bool CheckingIfExist(string login)
        {
            bool result = false;
            Conn();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM '" + tablename + "' WHERE login='" + login + "';", connection);
            string newpass = (String)command.ExecuteScalar();
            if (newpass != null)
                result = true;

            connection.Close();
            return result;
        }

        public bool SendPassword(string login)
        {
            if (CheckingIfExist(login))
            {
                Conn();
                SQLiteCommand cmd = new SQLiteCommand("SELECT password FROM '" + tablename + "' WHERE login= '" + login + "';", connection);
                string pass = (String)cmd.ExecuteScalar();
                cmd = new SQLiteCommand("SELECT email FROM '" + tablename + "' WHERE login= '" + login + "';", connection);
                string email = (String)cmd.ExecuteScalar();

                SendToMail(login, pass, email);
                return true;
            }
            return false;
        }

        public void ChangePassword(string login, string pass)
        {
            Conn();
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText =
                    "UPDATE "+ tablename +" SET password = '"+ pass + "' WHERE login ='" + login +"';";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public bool CheckingPassword(string login, string password)
        {

            if (!CheckingIfExist(login))
                return false;
            Conn();
            bool result = false;
            SQLiteCommand cmd = new SQLiteCommand("SELECT password FROM '" + tablename + "' WHERE login= '" + login + "';", connection);
            string newpass = (String)cmd.ExecuteScalar();
            if (newpass == password)
                result = true;
            
            connection.Close();
            return result;
        }

        public void DeleteAll()
        {
            Conn();
            SQLiteCommand command = new SQLiteCommand("DELETE  FROM " + tablename + ";", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void SelectAll()
        {
            Conn();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM '" + tablename + "';", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                string id = record["login"].ToString();
                string value = record["password"].ToString();
                string email = record["email"].ToString();
                string result = "" + id + " " + value + " " + email;
                Console.WriteLine(result);
            }
            connection.Close();
        }

        private void SendToMail(string login, string pass, string email)
        {
            var fromAddress = new MailAddress("tempuserscreen@gmail.com", "From administration");
            var toAddress = new MailAddress(email, "To " + login);
            string fromPassword = "4815162342qwerty";
            string subject = "Password retrieval";
            string body = String.Format("Hello "+login+"! Your password is " + pass);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
