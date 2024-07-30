using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity.Settlement
{
    [Table("xt_qxkz")]
    public class SysMedicineAuthorityEntity : IEntity<SysMedicineAuthorityEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string qxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string qxCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string qxmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rel_lxcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string rel_value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rel_qxId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }
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
        public string memo { get; set; }
        public string rel_lxywcode { get; set; }
        public string rel_lxywdesc { get; set; }
    }
}
