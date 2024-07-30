using Hangfire;
using Hangfire.Dashboard.Management.Metadata;
using Hangfire.Server;
using System.ComponentModel;

namespace NewtouchHIS.HangFire.UI
{
    [ManagementPage("任务管理")]
    public class JobConfig
    {
        [Hangfire.Dashboard.Management.Support.Job] //必须要声明这个属性 

        [DisplayName("任务标题")]

        [Description("任务描述")]

        [AutomaticRetry(Attempts = 0)] //重试次数

        [DisableConcurrentExecution(90)]//禁用并行执行

        public string Test(PerformContext context, IJobCancellationToken token,

           [DisplayData("Output Text", "Enter text to output.")] string outputText,

           [DisplayData("Bool类型参数", "Bool展示名称")] bool repeat,

           [DisplayData("Test Date", "Enter date")] DateTime testDate)

        {

            return (testDate + ":" + outputText);

        }
    }
}
