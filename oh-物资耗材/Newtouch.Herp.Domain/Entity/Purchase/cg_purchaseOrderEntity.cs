using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// 采购_采购计划
    /// </summary>
    [Table("cg_purchaseOrder")]
    public class CgPurchaseOrderEntity : IEntity<CgPurchaseOrderEntity>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 采购计划单号
        /// </summary>
        public string cgdh { get; set; }

        /// <summary>
        /// 入库部门Code
        /// </summary>
        public string rkbmCode { get; set; }

        /// <summary>
        /// 审核状态 0-待审核； 1-审核通过； 2-审核不通过；3-已作废；4-暂存
        /// </summary>
        public int auditState { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 状态 0:作废；1.有效
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建用户ID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户ID
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}