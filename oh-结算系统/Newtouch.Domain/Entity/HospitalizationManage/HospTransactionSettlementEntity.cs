using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 住院交易结算
    /// </summary>
    [Table("zy_jyjs")]
    public class HospTransactionSettlementEntity : IEntity<HospTransactionSettlementEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int jsnm { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 业务上保证多病人性质结算，关联结算内码唯一。来自住院结算表结算内码
        /// </summary>
        public int gljsnm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 病人性质组合子病人性质
        /// </summary>
        public string sbrxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zyts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zje { get; set; }

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
        public decimal flzffy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal jzfy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal xjzf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal xjwc { get; set; }

        /// <summary>
        /// ########CS系统没用到
        /// </summary>
        public decimal zhjz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xjzffs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 1 已结 2 已退
        /// </summary>
        public string jszt { get; set; }

        /// <summary>
        /// 撤销本次结算的结算（金额为负）所使用的内码
        /// </summary>
        public int cxjsnm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cxjsyy { get; set; }

        /// <summary>
        /// ########CS系统没用到
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// 1 结算 2 中途结算
        /// </summary>
        public string jsxz { get; set; }

        /// <summary>
        /// 只对中途结算有意义
        /// </summary>
        public DateTime? jsksrq { get; set; }

        /// <summary>
        /// 只对中途结算有意义
        /// </summary>
        public DateTime? jsjsrq { get; set; }

        /// <summary>
        /// 地税、国税发票上的发票代码，由于需要导出发票上报，所以必须   2013-8-7 作废此字段：采用财务发票管理（cw_fp）的程序录入发票号码和发票代码的起始号，然后通过两个号码的差数来计算每张发票的发票编码，同时计算出作废发票清单
        /// </summary>
        public string fpdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal jmje { get; set; }

        /// <summary>
        ///             yb_deal_wjy = 0,//无医保交易               yb_deal_mzgh = 1,//门诊挂号               yb_deal_mzsf = 2,//门诊收费               yb_deal_dbgh = 3,//大病挂号               yb_deal_dbsf = 4,//大病收费               yb_deal_jcsf = 5,//家床收费                yb_deal_zysf = 6,    //住院收费               yb_deal_gsgh = 7,    //工伤挂号               yb_deal_gsmz = 8,   //工伤收费               yb_deal_sssf = 9 //明细项目实时收费              
        /// </summary>
        public string jylx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? ysk { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? zl { get; set; }

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
