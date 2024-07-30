using System;

namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// 被冻结批次信息
    /// </summary>
    public class FrozenBatchesDetailVEntity
    {
        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 药库单位进价
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// 被冻结库存
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// 失败信息
        /// </summary>
        public string errorMsg { get; set; }
    }
}
