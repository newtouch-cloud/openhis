using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 住院结算支付方式
    /// </summary>
    [Table("zy_jszffs")]
    public class HospSettlementPaymentModelEntity : IEntity<HospSettlementPaymentModelEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int zyjszffsbh { get; set; }

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
        /// 
        /// </summary>
        public decimal zfje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ssry { get; set; }

        /// <summary>
        /// 
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
