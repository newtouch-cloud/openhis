using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 门诊挂号作废
    /// </summary>
    [Table("mz_ghzf")]
    public class OutpatientRegistAbandonEntity : IEntity<OutpatientRegistAbandonEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        public string zfry { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ghnm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? zfrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? zfrybh { get; set; }

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
    }
}
