using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("mz_cyzd")]
    public class UsedDiagnosisEntity : IEntity<UsedDiagnosisEntity>
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
        /// 1 西医诊断，2 中医诊断
        /// </summary>
        public int? type { get; set; }

        public string ksCode { get; set; }

        /// <summary>
        /// 使用次数
        /// </summary>
        public int? sycs { get; set; }
        /// <summary>
        /// 关联base库 系统诊断表
        /// </summary>
        public string zdCode { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        public string zdmc { get; set; }

        public string icd10 { get; set; }

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
