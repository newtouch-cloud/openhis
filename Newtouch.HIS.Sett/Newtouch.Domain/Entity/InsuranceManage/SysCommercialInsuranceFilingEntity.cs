using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 商保备案表
    /// </summary>
    [Table("xt_sbbab")]
    public class SysCommercialInsuranceFilingEntity : IEntity<SysCommercialInsuranceFilingEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string sbbabId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int patId { get; set; }

        public string bxCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ksrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime jsrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int zcs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int sycs { get; set; }

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
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

    }
}
