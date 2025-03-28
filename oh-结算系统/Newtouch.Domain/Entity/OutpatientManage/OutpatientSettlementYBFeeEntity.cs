using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 门诊结算医保交易费用
    /// </summary>
    [Table("mz_js_ybjyfy")]
    public class OutpatientSettlementYBFeeEntity : IEntity<OutpatientSettlementYBFeeEntity>
    {
        /// <summary>
        /// 费用内码
        /// </summary>
        [Key]
        public int ybfynm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int jsnm { get; set; }
        /// <summary>
        /// 医保结算号（医保返回的）
        /// </summary>
        public string ybjsh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 总费用
        /// </summary>
        public decimal? ZFY { get; set; }
        /// <summary>
        /// 现金支付
        /// </summary>
        public decimal? XJZF { get; set; }
        /// <summary>
        /// 一般费用（居保：医保费用）
        /// </summary>
        public decimal? YBFY { get; set; }
        /// <summary>
        /// 特殊费用
        /// </summary>
        public decimal? TSFY { get; set; }
        /// <summary>
        /// 基本支付（居保：账户支付）
        /// </summary>
        public decimal? JBZF { get; set; }
        /// <summary>
        /// 公补支付
        /// </summary>
        public decimal? GBZF { get; set; }
        /// <summary>
        /// 门诊自负段一般可报费用累计(居保起付线)
        /// </summary>
        public decimal? SUMZFDYBFY { get; set; }
        /// <summary>
        /// 门诊自负段内一般可报费用（居保：本次门诊自负段）
        /// </summary>
        public decimal? ZFDYBFY { get; set; }
        /// <summary>
        /// 基本余额
        /// </summary>
        public decimal? JBYE { get; set; }
        /// <summary>
        /// 公补余额
        /// </summary>
        public decimal? GBYE { get; set; }
        /// <summary>
        /// 可到中心报销一般费用
        /// </summary>
        public decimal? KBXYBFY { get; set; }
        /// <summary>
        /// 可到中心报销特殊费用
        /// </summary>
        public decimal? KBXTSFY { get; set; }
        /// <summary>
        /// （居保）统筹支付
        /// </summary>
        public decimal? TCZF { get; set; }
        /// <summary>
        /// （居保）救助(优抚)支付
        /// </summary>
        public decimal? JZZF { get; set; }
        /// <summary>
        /// （居保）大病补充支付
        /// </summary>
        public decimal? DKC023 { get; set; }

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
