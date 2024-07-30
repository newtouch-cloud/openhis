namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 发药处方药品信息
    /// </summary>
    public class GetfyDetailCFList
    {
        /// <summary>
        /// 处方内码
        /// </summary>
        public string cfnm { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 处方明细ID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string yp { get; set; }
    }

    /// <summary>
    /// 发药处方明细
    /// </summary>
    public class FyCfmxRequest
    {
        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }
    }
}
