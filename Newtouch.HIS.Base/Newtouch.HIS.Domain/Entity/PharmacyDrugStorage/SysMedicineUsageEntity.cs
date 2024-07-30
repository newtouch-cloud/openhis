using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_ypyf")]
    public class SysMedicineUsageEntity : IEntity<SysMedicineUsageEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int yfId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

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

        public string yplx { get; set; } 

    }
}
