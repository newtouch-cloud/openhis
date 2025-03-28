using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统药品供应商
    /// </summary>
    [Table("xt_ypgys")]
    [Obsolete("please use the view")]
    public class SysMedicineSupplierEntity : IEntity<SysMedicineSupplierEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int gysId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gysCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gysmc { get; set; }

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
        public string Mcsx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Srm1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Srm2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Khyh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Yhzh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Dz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Yzbm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Lxr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Lxdh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Bz { get; set; }

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

    }
}
