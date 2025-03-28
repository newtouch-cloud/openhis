using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_ypcrkfs")]
    public class SysMedicineStorageIOModeEntity : IEntity<SysMedicineStorageIOModeEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int crkfsId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string crkfsCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string crkfsmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string crkbz { get; set; }

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
