using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonYiBaoApp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            asConsole();
            try
            {
                var urls = System.Configuration.ConfigurationManager.AppSettings["listen_urls"];
                if (string.IsNullOrWhiteSpace(urls))
                {
                    //log
                    LogHelper.LogError("无法获取配置文件的url");
                    return;
                }
                StartOptions options = new StartOptions();
                foreach (var url in urls.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    options.Urls.Add(url);
                }
                if (options.Urls.Count == 0)
                {
                    //log
                    LogHelper.LogError("无法获取监听url");
                    return;
                }
                IDisposable apihost = WebApp.Start<Startup>(options);
                //WebApp.Start<Startup>(options);

            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.Message + ex.InnerException);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run();
        }
        static void asConsole()
        {
            StartOptions options = new StartOptions();
            options.Urls.Add("http://localhost:5789/");
            //test http://localhost:5789/api/values
            WebApp.Start<Startup>(options);
        }
    }
}
