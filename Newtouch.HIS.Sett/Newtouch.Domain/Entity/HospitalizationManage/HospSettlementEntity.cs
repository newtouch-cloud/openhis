using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("zy_js")]
    public class HospSettlementEntity : IEntity<HospSettlementEntity>
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
        /// 
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string brxz { get; set; }

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
        /// 结算时应收（另收款）
        /// </summary>
        public decimal xjzf { get; set; }

        /// <summary>
        /// ########CS没用到 使用0
        /// </summary>
        public decimal xjwc { get; set; }

        /// <summary>
        /// ########CS没用到
        /// </summary>
        public decimal zhjz { get; set; }

        /// <summary>
        /// ########CS没用到
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
        /// 撤销结算原因
        /// </summary>
        public string cxjsyy { get; set; }

        /// <summary>
        /// 账户  ########CS没用到
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// 1 结算 2 中途结算 ########CS是用于标示 出院结算/中途结算
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
        /// ########CS系统已作废此字段
        /// </summary>
        public string fpdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal jmje { get; set; }

        /// <summary>
        ///             yb_deal_wjy = 0,//无医保交易               yb_deal_mzgh = 1,//门诊挂号               yb_deal_mzsf = 2,//门诊收费               yb_deal_dbgh = 3,//大病挂号               yb_deal_dbsf = 4,//大病收费               yb_deal_jcsf = 5,//家床收费                yb_deal_zysf = 6,    //住院收费               yb_deal_gsgh = 7,    //工伤挂号               yb_deal_gsmz = 8,   //工伤收费               yb_deal_sssf = 9, //明细项目实时收费                          nb_yb_fyjs_mz_T1 = 10,  //农保门诊结算               nb_yb_bc_ztjs = 11, //农保住院中途结算收费               nb_yb_fyjs_zy_T1 = 12,  //农保住院出院结算收费               yb_deal_gszy = 13, //住院工伤               yb_deal_jcjz = 14,//家床记账   
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
        /// 请求支付订单号
        /// </summary>
        public string OutTradeNo { get; set; }

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
        /// 收费日期
        /// </summary>
        public DateTime? sfrq { get; set; }

        /// <summary>
        /// 折扣比例
        /// </summary>
        public decimal? zkbl { get; set; }
        /// <summary>
        /// 医保交易流水号
        /// </summary>
        public string ybjslsh { get; set; }

    }
}
