using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_zd")]
    public class SysDiagnosisEntity : IEntity<SysDiagnosisEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int zdId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zdCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zdmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string icd10 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string icd10fjm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string wb { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 诊断类型 对应字典   DiagnosisType
        /// </summary>
        public string zdlx { get; set; }

        public string ybnhlx { get; set; }

        public string gjybdm { get; set; }

        public string zdCode_yb { get; set; }

        public string zdmc_yb { get; set; }
        /// <summary>
        /// 慢性病
        /// </summary>
        public string mxb { get; set; }
    }
}
