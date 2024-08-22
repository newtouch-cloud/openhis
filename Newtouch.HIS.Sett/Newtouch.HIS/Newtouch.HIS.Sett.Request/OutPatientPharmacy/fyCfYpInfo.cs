namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    /// <summary>
    /// 发药处方药品信息
    /// </summary>
    public class fyCfYpInfo
    {
        /// <summary>
        /// 处方内码
        /// </summary>
        public int cfnm { get; set; }

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
}
