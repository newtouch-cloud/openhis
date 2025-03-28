using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Zy_Ypyz")]
    public class HospMedicinalOrderEntity : IEntity<HospMedicinalOrderEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int yzId { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Xsrbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte Ts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Sl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short? Cls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Zlff { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Sjap { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ycqh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Pcmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Yl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Yldw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ys { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Kssj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? JSsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Lyyf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Yzxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Zt { get; set; }

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
        public DateTime LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModiFierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int YszyzID { get; set; }

    }
}
