using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统药品损益原因
    /// </summary>
    [Table("xt_ypsyyy")]
    public class SysMedicineProfitLossReasonEntity : IEntity<SysMedicineProfitLossReasonEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string syyyId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string syyy { get; set; }

        /// <summary>
        /// 0 报损，1 报溢
        /// </summary>
        public string sybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

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

    }
}
