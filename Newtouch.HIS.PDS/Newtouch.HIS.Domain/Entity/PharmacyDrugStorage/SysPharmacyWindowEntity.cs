using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统药房窗口
    /// </summary>
    [Table("xt_yfck")]
    [Obsolete("please use the view")]
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
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
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
