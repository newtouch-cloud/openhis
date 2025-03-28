﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.YibaoInterfaceManage
{
    public class Output_2207VO
    {
        public int jsnm { get; set; }
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
		public DateTime? setl_time { get; set; }

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
        public decimal? medfee_sumamt { get; set; }

        /// <summary>
        /// 16,2|全自费金额|数值型|  |Y|  
        /// </summary> 
        public decimal? fulamt_ownpay_amt { get; set; }

        /// <summary>
        /// 16,2|超限价自费费用|数值型|  |Y|  
        /// </summary> 
		public decimal? overlmt_selfpay { get; set; }

        /// <summary>
        /// 16,2|先行自付金额|数值型|  |Y|  
        /// </summary> 
        public decimal? preselfpay_amt { get; set; }

        /// <summary>
        /// 16,2|符合政策范围金额|数值型|  |Y|  
        /// </summary> 
		public decimal? inscp_scp_amt { get; set; }

        /// <summary>
        /// 16,2|实际支付起付线|数值型|  |  |  
        /// </summary> 
		public decimal? act_pay_dedc { get; set; }

        /// <summary>
        /// 16,2|基本医疗保险统筹基金支出|数值型|  |Y|  
        /// </summary> 
        public decimal? hifp_pay { get; set; }

        /// <summary>
        /// 5,4|基本医疗保险统筹基金支付比例|数值型|  |Y|  
        /// </summary> 
        public decimal? pool_prop_selfpay { get; set; }

        /// <summary>
        /// 16,2|公务员医疗补助资金支出|数值型|  |Y|  
        /// </summary> 
		public decimal? cvlserv_pay { get; set; }

        /// <summary>
        /// 16,2|企业补充医疗保险基金支出|数值型|  |Y  |  
        /// </summary> 
        public decimal? hifes_pay { get; set; }

        /// <summary>
        /// 16,2|居民大病保险资金支出|数值型|  |Y  |  
        /// </summary> 
        public decimal? hifmi_pay { get; set; }

        /// <summary>
        /// 16,2|职工大额医疗费用补助基金支出|数值型|  |Y  |  
        /// </summary> 
        public decimal? hifob_pay { get; set; }

        /// <summary>
        /// 16,2|医疗救助基金支出|数值型|  |Y  |  
        /// </summary> 
        public decimal? maf_pay { get; set; }

        /// <summary>
        /// 16,2|其他支出|数值型|  |Y  |  
        /// </summary> 
        public decimal? oth_pay { get; set; }

        /// <summary>
        /// 16,2|基金支付总额|数值型|  |Y|  
        /// </summary> 
        public decimal? fund_pay_sumamt { get; set; }

        /// <summary>
        /// 16,2|个人负担总金额|数值型|  |Y|  
        /// </summary> 
		public decimal? psn_part_amt { get; set; }

        /// <summary>
        /// 16,2|个人账户支出|数值型|  |Y|  
        /// </summary> 
        public decimal? acct_pay { get; set; }

        /// <summary>
        /// 16,2|个人现金支出|数值型|  |Y|  
        /// </summary> 
        public decimal? psn_cash_pay { get; set; }

        /// <summary>
        /// 16,2|医院负担金额|数值型||Y|
        /// </summary> 
        public decimal? hosp_part_amt { get; set; }

        /// <summary>
        /// 16,2|余额|数值型||Y|
        /// </summary> 
		public decimal? balc { get; set; }

        /// <summary>
        /// 16,2|个人账户共济支付金额|数值型||Y|
        /// </summary> 
        public decimal? acct_mulaid_pay { get; set; }

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

        public string pch { get; set; }
        public string yllb { get; set; }
    }
}
