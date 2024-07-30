namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 发药明细
    /// </summary>
    public class fydisplayDetailInfo
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
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// mz_cfmxph 主键
        /// </summary>
        public string cfmxphId { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public int kcsl { get; set; }

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
        /// 金额
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
        /// 时间安排（频次）
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

        /// <summary>
        /// 0:已发药  1：已退药  -1：全部
        /// </summary>
        public string gjzt { get; set; }
    }
}
