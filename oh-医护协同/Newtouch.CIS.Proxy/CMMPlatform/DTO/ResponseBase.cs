namespace Newtouch.CIS.Proxy.CMMPlatform.DTO
{
    /// <summary>
    /// 公共响应报文
    /// </summary>
    public class ResponseBase
    {
        /// <summary>
        /// 响应体头部
        /// </summary>
        public Header Header { get; set; }

        /// <summary>
        /// 响应体结果
        /// </summary>
        public Result Result { get; set; }
    }
}