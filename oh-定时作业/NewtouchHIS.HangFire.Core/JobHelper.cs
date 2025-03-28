using Hangfire.HttpJob.Client;
using System.Net;

namespace NewtouchHIS.HangFire.Core
{
    public static class JobHelper
    {
        public async static Task<string> AddBackgroundJob(string serviceUrl)
        {
            //下面用的是同步的方式，也可以使用异步： await HangfireJobClient.AddBackgroundJobAsync
            var result = await HangfireJobClient.AddBackgroundJobAsync(serviceUrl, new BackgroundJob
            {
                JobName = "测试api",
                Method = "Get",
                Url = "http://localhost:20951/api/notice/getmsg/yy/hello",
                SendSuccess = true,
                DelayFromMinutes = 1, //这里是在多少分钟后执行
                QueueName = "default"
                //RunAt = new DateTime(2020,7,25,10,5,1) // 也可以不用指定 DelayFromMinutes 参数 直接指定在什么时候运行。
            }, new HangfireServerPostOption
            {
                BasicUserName = "admin",//这里是hangfire设置的basicauth
                BasicPassword = "test"//这里是hangfire设置的basicauth
            });

            return result.JobId;
            //result.JobId 就是这个一次性job的id 可以通过这个id在它没有运行前删除它
        }
        public async static Task<HttpJobResponse> AddBackgroundJobPost(HttpJobRequest request)
        {
            var backJobRequest = new BackgroundJob
            {
                JobName = request.JobName,
                Method = "Post",
                Url = request.ApiUrl,
                Data = request.PostData,
                SendSuccess = true,
                DelayFromMinutes = request.DelayFromMinutes ?? 10, //这里是在多少分钟后执行
                QueueName = request.QueueName ?? "default",
                //RunAt = new DateTime(2020,7,25,10,5,1) // 也可以不用指定 DelayFromMinutes 参数 直接指定在什么时候运行。
            };
            //成功回调
            if (request.SuccessCall != null && !string.IsNullOrWhiteSpace(request.SuccessCall.Url))
            {
                backJobRequest.Success = request.SuccessCall;
            }
            //失败回调
            if (request.FailedCall != null && !string.IsNullOrWhiteSpace(request.FailedCall.Url))
            {
                backJobRequest.Fail = request.FailedCall;
            }
            //下面用的是同步的方式，也可以使用异步： await HangfireJobClient.AddBackgroundJobAsync
            var result = await HangfireJobClient.AddBackgroundJobAsync(request.serviceUrl, backJobRequest, new HangfireServerPostOption
            {
                BasicUserName = "admin",//这里是hangfire设置的basicauth
                BasicPassword = "test"//这里是hangfire设置的basicauth
            });

            return new HttpJobResponse
            {
                ErrMessage = result.ErrMessage,
                JobId = result.JobId,
                IsSuccess = result.IsSuccess
            };
            //result.JobId 就是这个一次性job的id 可以通过这个id在它没有运行前删除它
        }

        public async static Task<HttpJobResponse> AddRecurringJobPost(HttpJobRequest request)
        {
            var result = HangfireJobClient.AddRecurringJob(request.ApiUrl, new RecurringJob()
            {
                JobName = request.JobName,
                Method = "Post",
                Data = request.PostData,
                Url = request.ApiUrl,
                SendSuccess = false,
                QueueName = request.QueueName ?? "recurring",
                Cron = request.Cron,
            }, new HangfireServerPostOption
            {
                BasicUserName = "admin",
                BasicPassword = "test"
            });
            return new HttpJobResponse
            {
                ErrMessage = result.ErrMessage,
                IsSuccess = result.IsSuccess
            };
        }
    }
}
