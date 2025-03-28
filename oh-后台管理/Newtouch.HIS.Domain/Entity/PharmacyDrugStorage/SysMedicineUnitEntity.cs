using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_ypdw")]
    public class SysMedicineUnitEntity : IEntity<SysMedicineUnitEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int ypdwId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypdwCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypdwmc { get; set; }

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
