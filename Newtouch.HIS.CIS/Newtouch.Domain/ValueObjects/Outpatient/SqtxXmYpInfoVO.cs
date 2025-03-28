namespace Newtouch.Domain.ValueObjects
{
    public class SqtxXmYpInfoVO
    {
        /// <summary>
        /// 医保代码
        /// </summary>
        public string ybdm { get; set; }
        /// <summary>
        /// 自付比例 事前提醒为一位，故定义为int
        /// </summary>
        public int zfbl { get; set; }
        /// <summary>
        /// 剂量
        /// </summary>
        public decimal jl { get; set; }
        /// <summary>
        /// 剂量单位
        /// </summary>
        public string jldw { get; set; }
        /// <summary>
        /// 门诊拆零单位
        /// </summary>
        public decimal mzcls { get; set; }
        /// <summary>
        /// 收费大类code
        /// </summary>
        public string sfdlCode { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal dj { get; set; } 
    }
}
