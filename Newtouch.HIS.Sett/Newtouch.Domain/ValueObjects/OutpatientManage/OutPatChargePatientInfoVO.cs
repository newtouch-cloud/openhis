namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// VO 门诊结算/退费 患者出院信息（医保结算号、社保编号、就诊原因、科室、医生、诊断）
    /// </summary>
    public class OutPatChargePatientInfoVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string ybjsh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sbbh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string jzyy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zdicd10 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zdmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ysmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ksmc { get; set; }
    }

    public class RefundZtDot
    {
        public int jsmxnm { get; set; }
        public string ztId { get; set; }
        public decimal dj { get; set; }
        public decimal sl { get; set; }
        public int cfnm { get; set; }
    }
}
