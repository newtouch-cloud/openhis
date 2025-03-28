namespace Newtouch.CIS.Proxy.CMMPlatform.DTO
{
    /// <summary>
    /// 响应体结果
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 1： 成功； 0：失败
        /// 必填
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 成功；失败(补充失败原因描述)
        /// 必填
        /// </summary>
        public string desc  {get; set; }
}
}