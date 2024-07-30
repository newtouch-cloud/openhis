using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 申领单 明细 查询
    /// </summary>
    public class RequisitionSelectDetailVO
    {
        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 有效日期
        /// </summary>
        public DateTime? Yxrq { get; set; }

        /// <summary>
        /// 申领数量
        /// </summary>
        public int slsl { get; set; }

        /// <summary>
        /// 申领数量单位
        /// </summary>
        public string slsldw { get; set; }

        /// <summary>
        /// 已发数量
        /// </summary>
        public int yfsl { get; set; }

        /// <summary>
        /// 已发数量单位
        /// </summary>
        public string yfsldw { get; set; }
    }
}
