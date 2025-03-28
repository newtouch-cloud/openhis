using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("V_S_xt_zd")]
    public class SysDiagnosisVEntity : IEntity<SysDiagnosisVEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        public int zdId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zdCode { get; set; }

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
        public string zdmc { get; set; }

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
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
