namespace Newtouch.CIS.Proxy.CMMPlatform.DTO
{
    /// <summary>
    /// 接收信息请求体
    /// </summary>
    public class Receive
    {
        /// <summary>
        /// 机构编码
        /// 必填
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// 就诊流水号
        /// 必填
        /// </summary>
        public string serialNo { get; set; }
    }
}