using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 门诊结算
    /// </summary>
    [Table("mz_js")]
    public class OutpatientSettlementEntity : IEntity<OutpatientSettlementEntity>
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
        public int patid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// 枚举EnumJslx 0 挂号 1 家床记帐 2 门诊结帐（包括家床结帐） 3 住院 4 账户收支   -------   在退费时，必须知道是否家床记帐，确定是否执行医保结算   －－－－   (原：0 挂号 1 处方 2 项目  因为处方和项目同时结算，故不分)   
        /// </summary>
        public string jslx { get; set; }

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
        /// 结算时应收（另结算现场需支付）
        /// </summary>
        public decimal xjzf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal xjwc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zhjz { get; set; }

        /// <summary>
        /// from xt_xjzffs //20180829作废，改依赖mz_jszffs表
        /// </summary>
        public string xjzffs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 1 已结（未退） 2 已退 3 补收 4 补收又退   --2006-06-17 启用,作为便于识别结算情况的冗余字段      --2006.06.16 暂时不用此字段   好像可以通过   已结：结算内码>0   已结（未退）：结算内码>0and撤销内码>0   已退：撤销结算内码>0
        /// </summary>
        public int jszt { get; set; }

        /// <summary>
        /// 撤销本次结算的结算（金额为负）所???用的内码
        /// </summary>
        public int cxjsnm { get; set; }

        /// <summary>
        /// 2012-11-24 需要修改账户支付方法，   帐户支付：个人、单位或银联等账户，可多账户分别支付，   	　由＂门诊－结算账户＂表来记录具体账户支付信息（待建）   	同时，删除”账户“字段         扣除记帐费用所用到账户
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// 地税、国税发票上的发票代码，由于需要导出发票上报，所以必须   2013-8-7 作废此字段：采用财务发票管理（cw_fp）的程序录入发票号码和发票代码的起始号，然后通过两个号码的差数来计算每张发票的发票编码，同时计算出作废发票清单
        /// </summary>
        public string fpdm { get; set; }

        /// <summary>
        /// （自费的折扣比例）
        /// </summary>
        public decimal jmbl { get; set; }

        /// <summary>
        /// （自费的折扣金额）
        /// </summary>
        public decimal jmje { get; set; }

        /// <summary>
        ///                医保交易类型 yb_deal_wjy = 0,//无医保交易               yb_deal_mzgh = 1,//门诊挂号               yb_deal_mzsf = 2,//门诊收费               yb_deal_dbgh = 3,//大病挂号               yb_deal_dbsf = 4,//大病收费               yb_deal_jcsf = 5,//家床收费                yb_deal_zysf = 6,    //住院收费               yb_deal_gsgh = 7,    //工伤挂号               yb_deal_gsmz = 8,   //工伤收费               yb_deal_sssf = 9 //明细项目实时收费                               nb_yb_fyjs_mz_T1 = 10,  //农保门诊结算              nb_yb_bc_ztjs = 11, //农保住院中途结算收费              nb_yb_fyjs_zy_T1 = 12,  //农保住院出院结算收费              yb_deal_gszy = 13, //住院工伤收费              yb_deal_jcjz = 14,//家床记账     
        /// </summary>
        public string jylx { get; set; }

        /// <summary>
        /// 预收款 应80 收100 找零20， 这里100
        /// </summary>
        public decimal? ysk { get; set; }

        /// <summary>
        /// 找零  应80 收100 找零20， 这里20
        /// </summary>
        public decimal? zl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? jch { get; set; }

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
        /// 
        /// </summary>
        public int ghnm { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 性别xb
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime? csny { get; set; }
        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 证件类型 
        /// </summary>
        public string zjlx { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// （收费日期）
        /// </summary>
        public DateTime? jzsj { get; set; }

        /// <summary>
        /// 退或被退都请置为1 方便查询
        /// </summary>
        public int? tbz { get; set; }

        /// <summary>
        /// 折后应收金额（自费部分打折后应收款金额）
        /// （貌似和xjzf字段意义重复了，故20180829作废）
        /// </summary>
        public decimal? zkhzje { get; set; }

        /// <summary>
        /// 是否是欠费预结
        /// </summary>
        public bool? isQfyj { get; set; }
        /// <summary>
        /// 请求支付订单号
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 医保结算流水号
        /// </summary>
        public string ybjslsh { get; set; }
        /// <summary>
        /// 结算是否使用减免
        /// </summary>
        public string isjm { get; set; }

    }
}
