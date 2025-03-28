using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("cw_fp")]
    public class FinancialInvoiceEntity : IEntity<FinancialInvoiceEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string fpdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string szm { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long qsfph { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long jsfph { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long dqfph { get; set; }

        /// <summary>
        /// 领用人员
        /// </summary>
        public DateTime lyrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lyry { get; set; }

        /// <summary>
        /// 0:无效; 1:有效
        /// </summary>
        public string zt { get; set; }
        
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

    }
}
