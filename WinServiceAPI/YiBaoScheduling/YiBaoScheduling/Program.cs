using Topshelf;

namespace Newtouch.HIS.Scheduling
{
    class Program
    {
        static void Main(string[] args)
        {
            //LoggerHelper.InitLog4net();

            HostFactory.Run(x =>
            {
                x.Service<ServiceRunner>();
                
                x.RunAsLocalSystem();

                x.SetDescription("Newtouch.HIS计划任务（Quartz+TopShelf实现Windows服务作业调度）");
                x.SetDisplayName("YiBaoScheduling");
                x.SetServiceName("YiBaoScheduling");

                x.EnablePauseAndContinue();

            });
        }
    }
}
