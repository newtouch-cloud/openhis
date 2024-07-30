using Microsoft.Owin.Hosting;
using System;
using System.Windows.Forms;

namespace ShangHaiYiBaoApp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
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
                LogHelper.LogError(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+ex.Message + ex.InnerException);
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
