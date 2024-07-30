using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai
{
	public class BJQInput_01
	{
		/// <summary>
		/// USB端口还是com端口  USB端口：1
		/// </summary>
		public int? comport { get; set; }
		public string context { get; set; }
		/// <summary>
		/// 语音控制字符串说明：
		/// 1234J 读音： “您好，请付款：壹千贰佰叁拾肆元”；
		/// 1234Y 读音： “收您：壹千贰佰叁拾肆元”；
		/// 1234Z 读音： “找您：壹千贰佰叁拾肆元”；
		/// W 读音： “您好，请稍等”；
		/// D 读音： “找零，请当面点清，谢谢”；
		/// X 读音： “谢谢”；
		/// 123.45G 读音：本次消费总额：*****.**元 ；
		/// 123.45H 读音：帐户余额：********.**元；
		/// 123．45E 读：********.**元；
		/// </summary>
		public string typemc { get; set; }

	}
}
