using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统医嘱频次
    /// </summary>
    [Table("xt_yzpc")]
    [Obsolete("please use the view")]
    public class SysMedicalOrderFrequencyEntity : IEntity<SysMedicalOrderFrequencyEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int yzpcId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yzpcCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yzpcmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int zxcs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int zxzq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zxzq2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zxzq4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zxsj { get; set; }

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

    }
}
