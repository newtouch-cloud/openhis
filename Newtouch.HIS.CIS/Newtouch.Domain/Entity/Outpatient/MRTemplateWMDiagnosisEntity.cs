using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_blmbxyzd")]
    public class MRTemplateWMDiagnosisEntity : IEntity<MRTemplateWMDiagnosisEntity>
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
        public string mbId { get; set; }

        /// <summary>
        /// 枚举 1 主诊断 2 辅诊断
        /// </summary>
        public int zdlx { get; set; }

        /// <summary>
        /// 关联base库 系统诊断表
        /// </summary>
        public string zdCode { get; set; }

        /// <summary>
        /// 冗余字段
        /// </summary>
        public string zdmc { get; set; }

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

        /// <summary>
        /// 疑似标志
        /// </summary>
        public bool ysbz { get; set; }

    }
}
