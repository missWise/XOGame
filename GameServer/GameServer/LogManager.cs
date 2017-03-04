using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public static class LogManager
    {
        private static string pathToLog = "server.log";

        public static void AddToLog(string name, string data)
        {
            try
            {
                var message = DateTime.Now.ToString() + " - " + name + ": " + data;
                StreamWriter sw = File.AppendText(pathToLog);
                sw.WriteLine(message);
                sw.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: AddToLog" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }
    }
}
