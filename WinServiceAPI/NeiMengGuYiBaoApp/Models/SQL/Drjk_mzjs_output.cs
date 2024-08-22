using NeiMengGuYiBaoApp.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.SQL
{
   public class Drjk_mzjs_output:SqlBase
    {
        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }


        /// <summary>
        /// 操作员
        /// </summary>
        public string czydm { get; set; }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime czrq { get; set; }
        /// <summary>
        /// 状态 1 正常 0作废
        /// </summary>
        public int zt { get; set; }
        /// <summary>
        /// 状态操作员
        /// </summary>
        public string zt_czy { get; set; }
        /// <summary>
        /// 状态日期
        /// </summary>
        public DateTime zt_rq { get; set; }


        /// <summary>
        /// 30|就诊ID|字符型|  |Y|  
        /// </summary> 
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 30|结算ID|字符型||Y|
        /// </summary> 
		public string setl_id { get; set; }

        /// <summary>
        /// 30|人员编号|字符型|  |Y|  
        /// </summary> 
		public string psn_no { get; set; }

        /// <summary>
        /// 50|人员姓名|字符型|  |Y|  
        /// </summary> 
		public string psn_name { get; set; }

        /// <summary>
        /// 6|人员证件类型|字符型|Y|Y|  
        /// </summary> 
		public string psn_cert_type { get; set; }

        /// <summary>
        /// 50|证件号码|字符型|  |Y|  
        /// </summary> 
		public string certno { get; set; }

        /// <summary>
        /// 6|性别|字符型|Y|  |  
        /// </summary> 
		public string gend { get; set; }

        /// <summary>
        /// 3|民族|字符型|Y|  |  
        /// </summary> 
		public string naty { get; set; }

        /// <summary>
        ///   |出生日期|日期型|  |  |yyyy-MM-dd
        /// </summary> 
		public string brdy { get; set; }

        /// <summary>
        /// 4,1|年龄|数值型|  |  |  
        /// </summary> 
		public string age { get; set; }

        /// <summary>
        /// 6|险种类型|字符型|Y|  |  
        /// </summary> 
		public string insutype { get; set; }

        /// <summary>
        /// 6|人员类别|字符型|Y|Y|  
        /// </summary> 
		public string psn_type { get; set; }

        /// <summary>
        /// 3|公务员标志|字符型|Y|Y|  
        /// </summary> 
		public string cvlserv_flag { get; set; }

        /// <summary>
        ///   |结算时间|日期时间型|  |Y|yyyy-MM-dd HH:mm:ss
        /// </summary> 
		public string setl_time { get; set; }

        /// <summary>
        /// 3|就诊凭证类型|字符型|Y|  |  
        /// </summary> 
		public string mdtrt_cert_type { get; set; }

        /// <summary>
        /// 6|医疗类别|字符型|Y|Y|  
        /// </summary> 
        public string med_type { get; set; }

        /// <summary>
        /// 16,2|医疗费总额|数值型|  |Y|  
        /// </summary> 
        public string medfee_sumamt { get; set; }

        /// <summary>
        /// 16,2|全自费金额|数值型|  |Y|  
        /// </summary> 
        public string fulamt_ownpay_amt { get; set; }

        /// <summary>
        /// 16,2|超限价自费费用|数值型|  |Y|  
        /// </summary> 
		public string overlmt_selfpay { get; set; }

        /// <summary>
        /// 16,2|先行自付金额|数值型|  |Y|  
        /// </summary> 
        public string preselfpay_amt { get; set; }

        /// <summary>
        /// 16,2|符合政策范围金额|数值型|  |Y|  
        /// </summary> 
		public string inscp_scp_amt { get; set; }

        /// <summary>
        /// 16,2|实际支付起付线|数值型|  |  |  
        /// </summary> 
		public string act_pay_dedc { get; set; }

        /// <summary>
        /// 16,2|基本医疗保险统筹基金支出|数值型|  |Y|  
        /// </summary> 
        public string hifp_pay { get; set; }

        /// <summary>
        /// 5,4|基本医疗保险统筹基金支付比例|数值型|  |Y|  
        /// </summary> 
        public string pool_prop_selfpay { get; set; }

        /// <summary>
        /// 16,2|公务员医疗补助资金支出|数值型|  |Y|  
        /// </summary> 
		public string cvlserv_pay { get; set; }

        /// <summary>
        /// 16,2|企业补充医疗保险基金支出|数值型|  |Y  |  
        /// </summary> 
        public string hifes_pay { get; set; }

        /// <summary>
        /// 16,2|居民大病保险资金支出|数值型|  |Y  |  
        /// </summary> 
        public string hifmi_pay { get; set; }

        /// <summary>
        /// 16,2|职工大额医疗费用补助基金支出|数值型|  |Y  |  
        /// </summary> 
        public string hifob_pay { get; set; }

        /// <summary>
        /// 16,2|医疗救助基金支出|数值型|  |Y  |  
        /// </summary> 
        public string maf_pay { get; set; }

        /// <summary>
        /// 16,2|其他支出|数值型|  |Y  |  
        /// </summary> 
        public string oth_pay { get; set; }

        /// <summary>
        /// 16,2|基金支付总额|数值型|  |Y|  
        /// </summary> 
        public string fund_pay_sumamt { get; set; }

        /// <summary>
        /// 16,2|个人负担总金额|数值型|  |Y|  
        /// </summary> 
		public string psn_part_amt { get; set; }

        /// <summary>
        /// 16,2|个人账户支出|数值型|  |Y|  
        /// </summary> 
        public string acct_pay { get; set; }

        /// <summary>
        /// 16,2|个人现金支出|数值型|  |Y|  
        /// </summary> 
        public string psn_cash_pay { get; set; }

        /// <summary>
        /// 16,2|医院负担金额|数值型||Y|
        /// </summary> 
        public string hosp_part_amt { get; set; }

        /// <summary>
        /// 16,2|余额|数值型||Y|
        /// </summary> 
		public string balc { get; set; }

        /// <summary>
        /// 16,2|个人账户共济支付金额|数值型||Y|
        /// </summary> 
        public string acct_mulaid_pay { get; set; }

        /// <summary>
        /// 30|医药机构结算ID|字符型||Y|存放发送方报文ID
        /// </summary> 
		public string medins_setl_id { get; set; }

        /// <summary>
        /// 6|清算经办机构|字符型||  |
        /// </summary> 
		public string clr_optins { get; set; }

        /// <summary>
        /// 6|清算方式|字符型|Y  |  |  
        /// </summary> 
        public string clr_way { get; set; }

        /// <summary>
        /// 6|清算类别|字符型|Y|Y  |
        /// </summary> 
        public string clr_type { get; set; }
    }
}
