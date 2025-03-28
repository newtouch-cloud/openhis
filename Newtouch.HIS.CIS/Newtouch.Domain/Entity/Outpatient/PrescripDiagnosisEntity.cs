using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("mz_clyzzd")]
    public class PrescripDiagnosisEntity : IEntity<PrescripDiagnosisEntity>
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
        /// cfh
        /// </summary>
        public string cfh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string jzId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cftag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string icd10 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zdmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yzpx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? yzlx { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

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
    }
}