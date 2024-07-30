using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("rpt_MonthReport_Detail")]
    public class RptMonthReportDetailEntity : IEntity<RptMonthReportDetailEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int year { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int month { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string patientType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zyhmzh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string brxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal dlje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }

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
        public string zt { get; set; }
    }
}
