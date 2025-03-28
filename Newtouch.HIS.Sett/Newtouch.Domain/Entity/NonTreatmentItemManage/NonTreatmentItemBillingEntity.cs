using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_fzlxmjfb")]
    public class NonTreatmentItemBillingEntity : IEntity<NonTreatmentItemBillingEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string jfbId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal je { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string patId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string smksCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string smry { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string tjr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cxry { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? cxrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cxjfbId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

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
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime jzrq { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal? dj { get; set; }

    }
}
