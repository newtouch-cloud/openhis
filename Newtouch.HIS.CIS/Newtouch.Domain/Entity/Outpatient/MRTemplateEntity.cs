using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_blmb")]
    public class MRTemplateEntity : IEntity<MRTemplateEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string mbId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int mblx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mbmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xbs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jws { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ct { get; set; }
        /// <summary>
        /// 处理
        /// </summary>
        public string clfa { get; set; }

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
        /// 科室模板
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string yjs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gms { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hy { get; set; }

    }
}
