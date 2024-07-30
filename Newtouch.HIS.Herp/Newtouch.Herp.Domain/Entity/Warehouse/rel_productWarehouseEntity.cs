using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// 物资库房
    /// </summary>
    [Table("rel_productWarehouse")]
    public class RelProductWarehouseEntity : IEntity<RelProductWarehouseEntity>
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
        /// 物资名称
        /// </summary>
        public string productName { get; set; }

        /// <summary>
        /// 库房ID(必填)
        /// </summary>
        public string warehouseId { get; set; }

        /// <summary>
        /// 库房名称(必填)
        /// </summary>
        public string warehouseName { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        public string unitId { get; set; }

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
