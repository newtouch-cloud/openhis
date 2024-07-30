using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 病区
    /// </summary>
    [Table("xt_bq")]
    public class SysWardEntity : IEntity<SysWardEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int bqId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bqmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

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
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }
        /// <summary>
        /// 科主任工号
        /// </summary>
        public string kzr_gh { get; set; }
        /// <summary>
        /// 护士长工号
        /// </summary>
        public string hsz_gh { get; set; }
       /// <summary>
       /// 康复治疗师工号
       /// </summary>
        public string kfzlsz_gh { get; set; }

    }
}
