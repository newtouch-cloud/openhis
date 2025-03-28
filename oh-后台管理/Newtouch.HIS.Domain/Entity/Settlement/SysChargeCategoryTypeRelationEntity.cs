using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 收费大类 大类类型 关联表
    /// </summary>
    [Table("xt_sfdl_lx")]
    public class SysChargeCategoryTypeRelationEntity : IEntity<SysChargeCategoryTypeRelationEntity>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 医疗机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 类型 关联字典ChargeCateType的字典项
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 收费大类编码
        /// </summary>
        public string dlCode { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

       

    }
}
