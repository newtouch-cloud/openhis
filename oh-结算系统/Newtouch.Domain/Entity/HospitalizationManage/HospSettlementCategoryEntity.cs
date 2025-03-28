using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 住院结算大类
    /// </summary>
    [Table("zy_jsdl")]
    public class HospSettlementCategoryEntity : IEntity<HospSettlementCategoryEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string jsdlId { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal kbfy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal flzffy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zlfy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zffy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal jmfy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime jsrq { get; set; }

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
