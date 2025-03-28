using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("jyjc_mbzt")]
    public class TemplateGroupPackageEntity : IEntity<TemplateGroupPackageEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string mbztId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mbId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mbmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ztId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ztmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

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

    }
}
