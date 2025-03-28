using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    ///门诊结算表
    /// </summary>
    [Table("mz_jsmx")]
    public class OutpatientSettlementDetailEntity : IEntity<OutpatientSettlementDetailEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int jsmxnm { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// 治疗项目处方明细内码
        /// </summary>
        public int? mxnm { get; set; }

        /// <summary>
        /// 药品处方明细内码
        /// </summary>
        public int? cf_mxnm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? sl { get; set; }

        /// <summary>
        /// 0 挂号 1 家床记帐 2 门诊结帐（包括家床结帐） 3 住院 4 账户收支   -------   在退费时，必须知道是否家床记帐，确定是否执行医保结算   －－－－   (原：0 挂号 1 处方 2 项目  因为处方和项目同时结算，故不分)   
        /// </summary>
        public string jslx { get; set; }

        /// <summary>
        /// 交易金额（dj*sl）
        /// </summary>
        public decimal? jyje { get; set; }

        /// <summary>
        /// 医保交易金额
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
        /// <summary>
        /// 可退数量
        /// </summary>
        public decimal? ktsl { get; set; }

    }
}
