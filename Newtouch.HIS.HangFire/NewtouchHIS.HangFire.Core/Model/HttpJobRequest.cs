using Hangfire.HttpJob.Client;

namespace NewtouchHIS.HangFire.Core
{
    public class HttpJobRequest
    {
        public string serviceUrl { get; set; }
        public string JobName { get; set; }
        public string? Cron { get; set; }
        public string Method { get; set; } = "Get";
        public string? ApiUrl { get; set; }
        public string? QueueName { get; set; } = "default";
        public bool? SendSuccess { get; set; } = true;
        public int? DelayFromMinutes { get; set; } = 0;
        public object? PostData { get; set; }
        public HttpCallbackJob? SuccessCall { get; set; }
        public HttpCallbackJob? FailedCall { get; set; }
    }

    public class HttpJobAuthUser
    {
        public string? BasicUserName { get; set;}
        public string? BasicPassword { get; set;}
    }

    public class HttpJobResponse
    {
        public string? ErrMessage { get; set; } 
        public bool IsSuccess { get; set; }
        public string? JobId { get; set; }
    }
}
