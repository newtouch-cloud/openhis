namespace NewtouchHIS.HangFire.Core.Model
{
    public class HangfireSettings
    {
        public string ServerName { get; set; }
        public string TablePrefix { get; set; }
        public string StartUpPath { get; set; }
        public string ReadOnlyPath { get; set; }
        public List<string> JobQueues { get; set; }
        public HttpAuthInfo HttpAuthInfo { get; set; } = new HttpAuthInfo();
        public int WorkerCount { get; set; } = 40;
        public bool DisplayStorageConnectionString { get; set; } = false;
        public JobAppAPIHost AppAPIHost { get; set; }
    }
    /// <summary>
    /// 各系统接口配置
    /// </summary>
    public class JobAppAPIHost
    {
        public string JobHost { get; set; }
        /// <summary>
        /// 接口中心
        /// </summary>
        public string? SiteApiManageHost { get; set; }
        /// <summary>
        /// 消息中心接口
        /// </summary>
        public string? SiteNoticeCenterHost { get; set; }
    }
}
