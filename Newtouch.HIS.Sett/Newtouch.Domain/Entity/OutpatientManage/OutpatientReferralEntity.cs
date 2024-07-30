using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 门诊转诊记录
    /// </summary>
    [Table("mz_zzjl")]
    public class OutpatientReferralEntity : IEntity<OutpatientReferralEntity>
    {
        /// <summary>
        /// 
        /// </summary>

        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? ghnm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? patid { get; set; }
        public string mzh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ys { get; set; }
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
        /// 转诊原因
        /// </summary>
        public string zzyy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 排班id
        /// </summary>
        public string scheduid { get; set; }
    }
}
