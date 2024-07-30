using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// 物资单位
    /// </summary>
    [Table("rel_productUnit")]
    public class RelProductUnitEntity : IEntity<RelProductUnitEntity>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 物资ID(必填)
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// 单位ID，wz_untiI表主键(必填)
        /// </summary>
        public string unitId { get; set; }

        /// <summary>
        /// 单位名称(必填)
        /// </summary>
        public string unit { get; set; }

        /// <summary>
        /// 转化因子(必填)
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 状态  0:作废；1.有效
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorCode { get; set; }

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
