using PastebinAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Huawei_Router_Tool_GUI
{
    class Logger
    {
        /**
         * Log file path
         *  
         * @var string
         */
        private static string __log_file_path;
        static string logFile;
        /**
         * __log_file_path get/set
         */
        public static string filePath
        {
            get { return Logger.__log_file_path; }
            set { if (value.Length > 0) Logger.__log_file_path = value; }
        }

        /**
         * Flush log file contents
         *  
         * @return void
         */
        public static void flush()
        {
            File.WriteAllText(Logger.filePath, string.Empty);
        }

        /**
         * Log message
         *  
         * @param string msg
         * @return void
         */
        public static void log(string msg)
        {
            Logger.filePath = Path.GetTempPath() + @"\HuaweiRouterTool\routerLog.txt";

            if (msg.Length > 0)
            {
                using (StreamWriter sw = File.AppendText(Logger.filePath))
                {
                    sw.WriteLine("{0} : {1}", DateTime.Now.ToShortDateString(),  msg);
                    sw.Flush();
                }
            }
        }

        public static void logSignal(string msg)
        {
            Logger.filePath = Path.GetTempPath() + @"\HuaweiRouterTool\Signal Log.txt";

            if (msg.Length > 0)
            {
                using (StreamWriter sw = File.AppendText(Logger.filePath))
                {
                    sw.WriteLine("{0} >> {1}", DateTime.Now.ToLocalTime(), msg);
                    sw.Flush();
                }
            }
        }

       
    }
}
