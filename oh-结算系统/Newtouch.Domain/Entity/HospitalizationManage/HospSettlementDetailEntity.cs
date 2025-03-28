using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 住院结算明细
    /// </summary>
    [Table("zy_jsmx")]
    public class HospSettlementDetailEntity : IEntity<HospSettlementDetailEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int jsmxbh { get; set; }

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
        public int? xmjfbbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fzlxmjfbId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ypjfbbh { get; set; }

        /// <summary>
        /// 1 药品   2 项目
        /// </summary>
        public string yzlx { get; set; }

        /// <summary>
        /// 交易金额（dj*sl）
        /// </summary>
        public decimal? jyje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? jyfwje { get; set; }

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
