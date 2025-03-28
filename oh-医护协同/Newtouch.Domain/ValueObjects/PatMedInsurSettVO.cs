using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    public class PatMedInsurSettVO
    {
        public string prejs_id { get; set; }
        public string mdtrt_id { get; set; }
        public string zyh { get; set; }
        public string psn_no { get; set; }
        public string psn_name { get; set; }
        public string setl_id { get; set; }
        public string certno { get; set; }
        public string gend { get; set; }
        public DateTime? brdy { get; set; }
        public decimal? age { get; set; }
        public string insutype { get; set; }
        /// <summary>
        /// 人员类别
        /// </summary>
        public string psn_type { get; set; }
        /// <summary>
        /// 公务员标志
        /// </summary>
        public string cvlserv_flag { get; set; }
        public DateTime? setl_time { get; set; }
        /// <summary>
        /// 就诊凭证类型
        /// </summary>
        public string mdtrt_cert_type { get; set; }
        /// <summary>
        /// 医疗类别
        /// </summary>
        public string med_type { get; set; }
        /// <summary>
        /// 医疗费总额
        /// </summary>
        public decimal? medfee_sumamt { get; set; }
        /// <summary>
        /// 全自费金额
        /// </summary>
        public decimal? fulamt_ownpay_amt { get; set; }
        /// <summary>
        /// 超限价自费费用
        /// </summary>
        public decimal? overlmt_selfpay { get; set; }
        /// <summary>
        /// 先行自付金额
        /// </summary>
        public decimal? preselfpay_amt { get; set; }
        /// <summary>
        /// 符合政策范围金额
        /// </summary>
        public decimal? inscp_scp_amt { get; set; }
        /// <summary>
        ///  实际支付起付线
        /// </summary>
        public decimal? act_pay_dedc { get; set; }
        /// <summary>
        /// 基本医疗保险统筹基金支出
        /// </summary>
        public decimal? hifp_pay { get; set; }
        /// <summary>
        /// 基本医疗保险统筹基金 支付比例
        /// </summary>
        public decimal? pool_prop_selfpay { get; set; }
        /// <summary>
        /// 公务员医疗补助资金支 出
        /// </summary>
        public decimal? cvlserv_pay { get; set; }
        /// <summary>
        /// 企业补充医疗保险基金 支出
        /// </summary>
        public decimal? hifes_pay { get; set; }
        /// <summary>
        /// 居民大病保险资金支出
        /// </summary>
        public decimal? hifmi_pay { get; set; }
        /// <summary>
        /// 职工大额医疗费用补助 基金支出
        /// </summary>
        public decimal? hifob_pay { get; set; }
        /// <summary>
        /// 医疗救助基金支出
        /// </summary>
        public decimal? maf_pay { get; set; }
        /// <summary>
        /// 医院负担金额
        /// </summary>
        public decimal? hosp_part_amt { get; set; }
        public decimal? oth_pay { get; set; }
        /// <summary>
        /// 基金支付总额
        /// </summary>
        public decimal? fund_pay_sumamt { get; set; }
        /// <summary>
        /// 个人负担总金额
        /// </summary>
        public decimal? psn_part_amt { get; set; }
        /// <summary>
        /// 个人账户支出
        /// </summary>
        public decimal? acct_pay { get; set; }
        /// <summary>
        /// 个人现金支出
        /// </summary>
        public decimal? psn_cash_pay { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public decimal? balc { get; set; }
        /// <summary>
        /// 个人账户共济支付金额
        /// </summary>
        public decimal? acct_mulaid_pay { get; set; }
        /// <summary>
        /// 医药机构结算 ID
        /// </summary>
        public string medins_setl_id { get; set; }
        public string clr_optins { get; set; }
        public string clr_way { get; set; }
        public string clr_type { get; set; }
        public string czydm { get; set; }
        public DateTime? czrq { get; set; }
        public string mdtrt_cert_no { get; set; }
        public string mid_setl_flag { get; set; }
    }

    public class PatQfWarnVo
    {
        public string zyh { get; set; }
        public string brxzdm { get; set; }
        public decimal? zje { get; set; }
        public decimal? zhye { get; set; }
        public decimal? yjfy { get; set; }
        public decimal? bje { get; set; }
        public decimal? ybxjzf { get; set; }
        public decimal? zhsy { get; set; }
    }
}
