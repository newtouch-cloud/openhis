using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
	public class OutPatChongQingVO
	{
		/// <summary>
		/// 门诊号
		/// </summary>
		public string mzh { get; set; }
		/// <summary>
		/// 结算内码
		/// </summary>
		public int jsnm { get; set; }

		/// <summary>
		/// 收费日期
		/// </summary>
		public DateTime? sfrq { get; set; }

		/// <summary>
		/// 结算人员
		/// </summary>
		public string CreatorCode { get; set; }

		/// <summary>
		/// 结算时间
		/// </summary>
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// 结算人员姓名
		/// </summary>
		public string CreatorUserName { get; set; }

		/// <summary>
		/// 门诊挂号 或 门诊收费记账
		/// </summary>
		public string jslx { get; set; }

		/// <summary>
		/// 发票号
		/// </summary>
		public string fph { get; set; }

		/// <summary>
		/// 医保结算 医保结算号
		/// </summary>
		public string ybjsh { get; set; }

		/// <summary>
		/// 总金额
		/// </summary>
		public decimal jszje { get; set; }

		/// <summary>
		/// 结算支付
		/// </summary>
		public decimal jsxjzf { get; set; }
		/// <summary>
		/// 现金支付方式名称
		/// </summary>
		public string xjzffsmc { get; set; }
        /// <summary>
        /// 挂号来源
        /// </summary>
        public string ghly { get; set; } 
        public string jzid { get; set; }
        public string pch { get; set; }
        public string yllb { get; set; }
        public string bzbm { get; set; }
    }
}
