using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统药品临时损益信息
    /// </summary>
    [Table("xt_yp_ls_syxx")]
    public class SysMedicineTemporaryProfitLossEntity : IEntity<SysMedicineTemporaryProfitLossEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string syId { get; set; }

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
        public string Ypdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Yxq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Xykc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Sysl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Dw { get; set; }

        /// <summary>
        /// 批发价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Pfj { get; set; }

        /// <summary>
        /// 零售价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Lsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Syyy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Czy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Zrr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Djh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Jjm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pc { get; set; }

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
