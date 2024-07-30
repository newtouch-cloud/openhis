using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 查询已结转的药品
    /// </summary>
    public class CarryOverMedicineVO
    {
        /// <summary>
        /// 结转时间
        /// </summary>
        public string Jzsj { get; set; }

        /// <summary>
        /// 账期
        /// </summary>
        public string zq { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// 结转数量
        /// </summary>
        public int? jzsl { get; set; }

        /// <summary>
        /// 结转数量 带单位
        /// </summary>
        public string jzslstr { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 频次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 药厂名称/生产厂家
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// 批发价
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// 零售价
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// 批发金额
        /// </summary>
        public decimal? pfze { get; set; }

        /// <summary>
        /// 零售金额
        /// </summary>
        public decimal? lsze { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
