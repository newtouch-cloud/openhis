using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
   public class Input_3202:InputBase
    {
        public data3202 data { get; set; }
    }

    public class data3202
    {

		/// <summary>
		/// 清算类别
		/// </summary>
		public string clr_type { get; set; }

		/// <summary>
		/// 1|结算经办机构|字符型|6|  |Y  |  
		/// </summary> 
		public string setl_optins { get; set; }

        /// <summary>
        /// 2|文件查询号 |字符型 |30 ||Y  |上传明细文件后返回的号码
        /// </summary> 
		public string file_qury_no { get; set; }

        /// <summary>
        /// 3|对账开始日期 |日期型 |  |  |Y  |yyyy-MM-dd
        /// </summary> 
		public string stmt_begndate { get; set; }

        /// <summary>
        /// 4|对账结束日期|日期型|  |  |Y  |yyyy-MM-dd
        /// </summary> 
		public string stmt_enddate { get; set; }

        /// <summary>
        /// 5|医疗费总额|数值型|16,2|  |Y|  
        /// </summary> 
		public decimal medfee_sumamt { get; set; }

        /// <summary>
        /// 6|基金支付总额|数值型|16,2|  |Y|  
        /// </summary> 
		public decimal fund_pay_sumamt { get; set; }

        /// <summary>
        /// 7|现金支付金额|数值型|16,2|  |Y|  
        /// </summary> 
		public decimal cash_payamt { get; set; }

        /// <summary>
        /// 8|定点医药机构结算笔数|数值型|10|  |Y  |  
        /// </summary> 
		public int fixmedins_setl_cnt { get; set; }

		/// <summary>
		/// 退费结算标志
		/// </summary>
		//public string refd_setl_flag { get; set; }

	}
}
