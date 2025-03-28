
using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 需要结转的药品信息
    /// </summary>
    public class NeedCarryOverMedicineVO
    {
        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// 药库数量
        /// </summary>
        public int? kcsl { get; set; }

        /// <summary>
        /// 药库批发价
        /// </summary>
        public Decimal? ykpfj { get; set; }

        /// <summary>
        /// 药库零售价
        /// </summary>
        public decimal yklsj { get; set; }

        /// <summary>
        /// 药库进价
        /// </summary>
        public decimal? jj { get; set; }

        /// <summary>
        /// 转换因子
        /// </summary>
        public int zhyz { get; set; }
    }
}
