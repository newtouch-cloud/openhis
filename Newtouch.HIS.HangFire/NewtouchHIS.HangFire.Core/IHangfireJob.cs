using Hangfire;
using Hangfire.HttpJob.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackgroundJob = Hangfire.HttpJob.Client.BackgroundJob;

namespace NewtouchHIS.HangFire.Core
{
    public interface IHangfireJob
    {
        Task<string> AddBackgroundJob(string serverUrl);
    }

    public class HangfireJob:IHangfireJob
    {
        public async Task<string> AddBackgroundJob(string serverUrl)
        {
            //下面用的是同步的方式，也可以使用异步： await HangfireJobClient.AddBackgroundJobAsync
            var result = await HangfireJobClient.AddBackgroundJobAsync(serverUrl, new BackgroundJob
            {
                JobName = "测试api",
                Method = "Get",
                Url = "http://localhost:20951/api/notice/getmsg/yy/hello",
                SendSuccess = true,
                DelayFromMinutes = 1 //这里是在多少分钟后执行
                                     //RunAt = new DateTime(2020,7,25,10,5,1) // 也可以不用指定 DelayFromMinutes 参数 直接指定在什么时候运行。
            }, new HangfireServerPostOption
            {
                BasicUserName = "admin",//这里是hangfire设置的basicauth
                BasicPassword = "test"//这里是hangfire设置的basicauth
            });

            return result.JobId;
            //result.JobId 就是这个一次性job的id 可以通过这个id在它没有运行前删除它
        }

        public async Task<bool> RemoveBackgroundJob(string serverUrl,string jobId)
        {
            var result = HangfireJobClient.RemoveBackgroundJob(serverUrl, jobId, new HangfireServerPostOption
            {
                BasicUserName = "admin",//这里是hangfire设置的basicauth
                BasicPassword = "test"//这里是hangfire设置的basicauth
            });
            return result.IsSuccess;
        }
    }

   
}
