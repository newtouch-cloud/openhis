using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 门诊结算支付方式
    /// </summary>
    [Table("mz_jszffs")]
    public class OutpatientSettlementPaymentModelEntity : IEntity<OutpatientSettlementPaymentModelEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int mzjszffsbh { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// from xt_xjzffs
        /// </summary>
        public string xjzffs { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal zfje { get; set; }

        /// <summary>
        /// 作废不用
        /// </summary>
        public string ssry { get; set; }

        /// <summary>
        /// 作废不用
        /// </summary>
        public DateTime? ssrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? zh { get; set; }

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
