using System;

namespace Newtouch.HIS.Domain.VO
{
    /// <summary>
    /// 退药明细
    /// </summary>
    public class RefundedDrugVo
    {
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
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 单价（部门单位）
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 药厂名称
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// 成组号
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// 数量 （部门单位数量） 默认与ktsl相同
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 可退数量 （部门单位数量） 默认与sl相同
        /// </summary>
        public int ktsl { get; set; }

        /// <summary>
        /// 部门单位
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 转化因子
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 数量+单位
        /// </summary>
        public string slstr { get; set; }

    }
}