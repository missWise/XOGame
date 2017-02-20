using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class DataBaseManager
    {
        string databaseName = @"C:\Users\Sherlock\Desktop\GameXoProd\GameServer\GameServer\bin\Debug\XOACCOUNTDB.db";
        string tablename = "PersonalData";
        SQLiteConnection connection;
        public DataBaseManager() { }
        public void Conn()
        {
            connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));
            connection.Open();
        }
        public void Insert(string login, string password)
        {
            Conn();
            SQLiteCommand command = new SQLiteCommand("INSERT INTO '" + tablename + "' ('login', 'password') VALUES ('" + login + "','" + password + "');", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public bool RA(string command, string login, string password)
        {
            bool result = false;
            switch (command)
            {
                case "reg":
                    if (!CheckingIfExist(login))
                    {
                        Insert(login, password);
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
        public void SelectAll()
        {
            Conn();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM '" + tablename + "';", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                string id = record["login"].ToString();
                string value = record["password"].ToString();
                string result = "" + id + value;
                Console.WriteLine(result);
            }
            connection.Close();
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

    }
}
