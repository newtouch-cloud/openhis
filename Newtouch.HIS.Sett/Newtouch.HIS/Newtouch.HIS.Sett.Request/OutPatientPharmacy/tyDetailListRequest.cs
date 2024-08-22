namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    /// <summary>
    /// 门诊退药显示药品详细信息
    /// </summary>
    public class tyDetailListRequest
    {
        /// <summary>
        /// 处方明细ID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// 频号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// 剂量
        /// </summary>
        public decimal jl { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// 用法
        /// </summary>
        public string yf { get; set; }

        /// <summary>
        /// 频次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 用量
        /// </summary>
        public decimal yl { get; set; }

        /// <summary>
        /// 用了单位
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yszt { get; set; }

        /// <summary>
        /// 排列号
        /// </summary>
        public short plh { get; set; }

        /// <summary>
        /// 处方内码
        /// </summary>
        public int cfnm { get; set; }
    }
}
