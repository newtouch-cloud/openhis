using FrameworkBase.MultiOrg.Domain.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 挂号费用
    /// </summary>
    [NotMapped]
    public class GHZGroupAndFeesVO
    {
        /// <summary>
        /// 挂号费
        /// </summary>
        public decimal ghfPrice { get; set; }

        /// <summary>
        /// 诊疗费
        /// </summary>
        public decimal zlfPrice { get; set; }

        /// <summary>
        /// 磁卡费
        /// </summary>
        public decimal ckfPrice { get; set; }

        /// <summary>
        /// 工本费
        /// </summary>
        public decimal gbfPrice { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal totalfees { get; set; }

        /// <summary>
        /// 关联项目的Entity
        /// </summary>
        public Dictionary<string, SysChargeItemVEntity> sfxmList { get; set; }
        public Dictionary<string, List<SysChargeItemVEntity>> sfxmzhList { get; set; }
        /// <summary>
        /// 排班Id
        /// </summary>
        public decimal? ScheduId { get; set; }
    }
}
