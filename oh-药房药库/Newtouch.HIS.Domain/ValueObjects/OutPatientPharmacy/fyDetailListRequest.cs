namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 接口返回处方明细初步信息
    /// </summary>
    public class fyDetailListRequest
    {
        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 处方明细ID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 处方内码
        /// </summary>
        public int cfnm { get; set; }

        /// <summary>
        /// 药厂名称
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// 剂量
        /// </summary>
        public decimal jl { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// 用法名称
        /// </summary>
        public string yfmc { get; set; }

        /// <summary>
        /// 时间安排 （频次）
        /// </summary>
        public string sjap { get; set; }

        /// <summary>
        /// 用量
        /// </summary>
        public decimal yl { get; set; }

        /// <summary>
        /// 用量单位
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yszt { get; set; }

        /// <summary>
        /// 频次
        /// </summary>
        public short plh { get; set; }
    }
}
