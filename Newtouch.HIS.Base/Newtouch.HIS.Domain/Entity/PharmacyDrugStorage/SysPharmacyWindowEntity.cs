using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_yfck")]
    public class SysPharmacyWindowEntity : IEntity<SysPharmacyWindowEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int yfckId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfckCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfckmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("已作废这种关联关系")]
        public string yfbmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pfyms { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string kqbz { get; set; }

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
