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
        string databaseName = @"D:\Programming\ORT\HomeWork44\GHXO\XOGame\XODatabase.db";
        string tablename = "PersonalData";
        SQLiteConnection connection;
        public DataBaseManager() { }
        public void Conn()
        {
            try
            {
                connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));
                connection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: Conn" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }
        public void Insert(string login, string password, string email)
        {
            try
            {
                Conn();
                SQLiteCommand command = new SQLiteCommand("INSERT INTO '" + tablename + "' ('login', 'password', 'email') VALUES ('" + login + "','" + password + "','" + email + "');", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: Insert" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }
        public bool RA(string command, string login, string password, string email)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("METHOD: RA" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }
        
        public bool CheckingIfExist(string login)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("METHOD: CheckingIfExist" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }

        public bool SendPassword(string login)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("METHOD: SendPassword" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }

        public void ChangePassword(string login, string pass)
        {
            try
            {
                Conn();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText =
                        "UPDATE " + tablename + " SET password = '" + pass + "' WHERE login ='" + login + "';";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: ChangePassword" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }

        public bool CheckingPassword(string login, string password)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("METHOD: CheckingPassword" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }

        public void DeleteAll()
        {
            try
            {
                Conn();
                SQLiteCommand command = new SQLiteCommand("DELETE  FROM " + tablename + ";", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: DeleteAll" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }
        public void SelectAll()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("METHOD: SelectAll" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }

        private void SendToMail(string login, string pass, string email)
        {
            try
            {
                var fromAddress = new MailAddress("tempuserscreen@gmail.com", "From administration");
                var toAddress = new MailAddress(email, "To " + login);
                string fromPassword = "4815162342qwerty";
                string subject = "Password retrieval";
                string body = String.Format("Hello " + login + "! Your password is " + pass);

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
            catch (Exception ex)
            {
                throw new Exception("METHOD: SendToMail" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }
    }
}