using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统药品盘点信息
    /// </summary>
    [Table("xt_yp_pdxx")]
    public class SysMedicineInventoryEntity : IEntity<SysMedicineInventoryEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string pdId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Kssj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Jssj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short? Pdfs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

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

    }
}
