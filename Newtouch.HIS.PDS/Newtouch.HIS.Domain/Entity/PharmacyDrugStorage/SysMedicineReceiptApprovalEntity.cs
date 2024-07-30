using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统药品单据审核
    /// </summary>
    [Table("xt_yp_djsh")]
    public class SysMedicineReceiptApprovalEntity : IEntity<SysMedicineReceiptApprovalEntity>
    {
        /// <summary>
        /// 单据审核ID
        /// </summary>
        [Key]
        public string djshId { get; set; }

        /// <summary>
        /// 组织结构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 出入库单据ID
        /// </summary>
        public string crkId { get; set; }

        /// <summary>
        /// 操作员工号
        /// </summary>
        public string Shczy { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime Shsj { get; set; }

        /// <summary>
        /// 操作员工号
        /// </summary>
        public string Qxczy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Qxsj { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

    }
}
