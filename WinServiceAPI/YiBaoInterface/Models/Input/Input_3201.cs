using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_3201 : InputBase
    {
        public data3201 data { get; set; }
    }

    public class data3201
    {
        /// <summary>
        /// 1|险种|字符型|6|Y|Y|
        /// </summary> 
        public string insutype { get; set; }

        /// <summary>
        /// 2|清算类别|字符型|6|Y|Y|
        /// </summary> 
		public string clr_type { get; set; }

        /// <summary>
        /// 3|结算经办机构|字符型|6|  |Y  |  
        /// </summary> 
		public string setl_optins { get; set; }

        /// <summary>
        /// 4|对账开始日期 |日期型 |  |  |Y  |  
        /// </summary> 
		public string stmt_begndate { get; set; }

        /// <summary>
        /// 5|对账结束日期 |日期型 |  |  |Y  |  
        /// </summary> 
		public string stmt_enddate { get; set; }

		/// <summary>
		/// 6|医疗费总额|数值型|16,2|  |Y|
		/// </summary> 
		public decimal? medfee_sumamt { get; set; }

		/// <summary>
		/// 7|基金支付总额|数值型|16,2|  |Y|
		/// </summary> 
		public decimal? fund_pay_sumamt { get; set; }

		/// <summary>
		/// 6|个人账户支付金额|数值型|16,2|  |Y|  
		/// </summary> 
		public decimal? acct_pay { get; set; }


		/// <summary>
		/// 8|定点医药机构结算笔数|数值型|10|  |Y  |  
		/// </summary> 
		public int fixmedins_setl_cnt { get; set; }

		public string refd_setl_flag { get; set; }
	}
}
