using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统药品单据
    /// </summary>
    [Table("xt_yp_dj")]
    public class SysMedicineReceiptEntity : IEntity<SysMedicineReceiptEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string ypdjId { get; set; }

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
        public string djmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string djqz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int djh { get; set; }

        /// <summary>
        /// 0:允许 1:不允许 -1:未选择
        /// </summary>
        public int xgdjh { get; set; }

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
