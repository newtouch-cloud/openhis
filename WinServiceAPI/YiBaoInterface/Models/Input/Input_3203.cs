using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
	public class Input_3203 : InputBase
	{
		public data3203 data { get; set; }
	}
	public class data3203
	{
		/// <summary>
		///1|clr_type|清算类别|字符型|字符型|30|Y|Y||
		/// </summary>
		public string clr_type { get; set; }
		/// <summary>
		///2|clr_way|清算方式|字符型|字符型|30|Y|||
		/// </summary>
		public string clr_way { get; set; }
		/// <summary>
		///3|setlym|清算年月|字符型|字符型|6||Y||
		/// </summary>
		public string setlym { get; set; }
		/// <summary>
		///4|psntime|清算人次|数值型|数值型|6||Y||
		/// </summary>
		public decimal psntime { get; set; }
		/// <summary>
		///5|medfee_sumamt|医疗费总额|数值型|数值型|16,2||Y||
		/// </summary>
		public decimal medfee_sumamt { get; set; }
		/// <summary>
		///6|med_sumfee|医保认可费用总额|数值型|数值型|16,2||Y||
		/// </summary>
		public decimal med_sumfee { get; set; }
		/// <summary>
		///7|fund_appy_sum|基金申报总额|数值型|数值型|16,2||Y||
		/// </summary>
		public decimal fund_appy_sum { get; set; }
		/// <summary>
		///8|cash_payamt|现金支付金额|数值型|数值型|16,2||Y||
		/// </summary>
		public decimal cash_payamt { get; set; }
		/// <summary>
		///9|acct_pay|个人账户支出|数值型|数值型|16,2||Y||
		/// </summary>
		public decimal acct_pay { get; set; }
		/// <summary>
		///10|begndate|开始日期|日期型|日期型|||Y|yyyy-MM-dd|
		/// </summary>
		public string begndate { get; set; }
		/// <summary>
		///11|enddate|结束日期|日期型|日期型|||Y|yyyy-MM-dd|
		/// </summary>
		public string enddate { get; set; }

	}
}
