using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Infrastructure.Log
{
    public class AppLogger
    {
        public static void Info(string message)
        {
            try
            {
                var root = "C:\\log_ypcg";
                var date = DateTime.Now.ToString("yyyyMMddHHmm");
                var dirPath = string.Format("{0}\\{1}", root, date.Substring(0, 8));
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                var filePath = string.Format("{0}\\{1}.txt", dirPath, date.Substring(8, 2));
                File.AppendAllText(filePath, string.Format("\r\n\r\n{0}.Info.{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message));
            }
            catch { }
        }
    }
}
