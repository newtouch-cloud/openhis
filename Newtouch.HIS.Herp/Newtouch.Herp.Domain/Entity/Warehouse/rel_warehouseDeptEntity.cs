using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// 库房科室
    /// </summary>
    [Table("rel_warehouseDept")]
    public class RelWarehouseDeptEntity : IEntity<RelWarehouseDeptEntity>
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
        /// 库房ID
        /// </summary>
        public string warehouseId { get; set; }

        /// <summary>
        /// 科室ID
        /// </summary>
        public string deptId { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string deptName { get; set; }

        /// <summary>
        /// 状态
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
