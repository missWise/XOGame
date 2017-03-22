using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerAPI sa = new ServerAPI();
            //string databaseName = @"D:\XODatabase.db";
            //SQLiteConnection.CreateFile(databaseName);
            //Console.WriteLine(File.Exists(databaseName) ? "База данных создана" : "Возникла ошиюка при создании базы данных");


            //SQLiteConnection connection =
            //new SQLiteConnection(string.Format("Data Source={0};", databaseName));
            //SQLiteCommand command =
            //    new SQLiteCommand("CREATE TABLE PersonalData (login TEXT, password TEXT, email TEXT);", connection);
            //connection.Open();
            //command.ExecuteNonQuery();
            //connection.Close();

            //DataBaseManager d = new DataBaseManager();
            ////////d.RA("reg", "lena", "111", "eo.pakhomova96@gmail.com");
            //////d.ChangePassword("lena", "111");
            //d.SelectAll();
            //Console.ReadKey(true);
        }
        
    }
}
