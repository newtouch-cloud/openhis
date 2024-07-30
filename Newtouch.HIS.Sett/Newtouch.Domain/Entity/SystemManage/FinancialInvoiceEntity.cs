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
        /// 发票单号
        /// </summary>
        [Key]
        public string fpdm { get; set; }

        /// <summary>
        /// 首字母
        /// </summary>
        public string szm { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 起始发票号
        /// </summary>
        public long qsfph { get; set; }

        /// <summary>
        /// 结束发票号
        /// </summary>
        public long jsfph { get; set; }

        /// <summary>
        /// 当前发票号
        /// </summary>
        public long dqfph { get; set; }

        /// <summary>
        /// 领用日期
        /// </summary>
        public DateTime lyrq { get; set; }

        /// <summary>
        /// 领用人员
        /// </summary>
        public string lyry { get; set; }

        /// <summary>
        /// 0:停用; 1:启用
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
        /// 排序
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 0：有效；1；作废
        /// </summary>
        public int? is_del { get; set; }

    }
}
