using System;

namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// 申领单明细
    /// </summary>
    public class ApplyDetailVEntity
    {
        /// <summary>
        /// 申领单明细ID
        /// </summary>
        public string sldmxId { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 收费大类名称
        /// </summary>
        public string dlmc { get; set; }

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
        /// 申领数量 带单位
        /// </summary>
        public string slslStr { get; set; }

        /// <summary>
        /// 申领数量 最小单位
        /// </summary>
        public int slsl { get; set; }

        /// <summary>
        /// 已发数量 带单位
        /// </summary>
        public string yfslStr { get; set; }

        /// <summary>
        /// 已发数量 最小单位
        /// </summary>
        public int yfsl { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
