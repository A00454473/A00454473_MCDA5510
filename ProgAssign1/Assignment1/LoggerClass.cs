using System;
using System.IO;

namespace Assignment1
{
    class LoggerClass
    {

        public static bool Log(string strMessage)
        {
            try
            {
                string Logpath = "..\\..\\..\\..\\logs";
                string fileName = "log_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", Logpath, fileName), FileMode.Append, FileAccess.Write);
                StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
                objStreamWriter.WriteLine(strMessage);
                objStreamWriter.Close();
                objFilestream.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
