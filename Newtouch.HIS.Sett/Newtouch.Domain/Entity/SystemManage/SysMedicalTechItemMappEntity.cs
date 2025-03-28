using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 医技项目对照
    /// </summary>
    [Table("xt_yjxmdz")]
    public class SysMedicalTechItemMappEntity : IEntity<SysMedicalTechItemMappEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int yjxmdzbh { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ks{get;set;}

        /// <summary>
        /// 
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// 0 只允许确认不允许收费  1 允许确认和收费
        /// </summary>
        public string ksbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

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
        public int? px { get; set; }

    }
}
