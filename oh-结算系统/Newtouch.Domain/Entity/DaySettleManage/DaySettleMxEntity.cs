using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_daysettlemx")]
    public class DaySettleMxEntity : IEntity<DaySettleMxEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jslx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zffs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? je { get; set; }

        /// <summary>
        /// 医嘱性质 1临时 2长期
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 医嘱类型 1药品 2治疗项目
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
    }
}
