using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_bl")]
    public class MedicalRecordEntity : IEntity<MedicalRecordEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string blId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? fbsj { get; set; }

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
        public string clfa { get; set; }
        public string fzjc { get; set; }
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
