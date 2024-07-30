using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("rpt_mz_rjb")]
    public class RptrptMzRjbEntity : IEntity<RptrptMzRjbEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

		public DateTime kssj { get; set; }

	    public DateTime jssj { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string czr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? zje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? xjzf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

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
