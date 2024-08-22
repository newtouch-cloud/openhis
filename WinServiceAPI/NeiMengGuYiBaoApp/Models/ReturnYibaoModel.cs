using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiBaoInterface.Models
{
	public class ReturnYibaoModel<O>
	{
		/// <summary>
		/// 执行代码（1成功  -1失败）
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// 错误说明
		/// </summary>
		public string ErrorMsg { get; set; }

		/// <summary>
		/// 返回结果
		/// </summary>
		public O Data { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public ReturnYibaoModel()
		{
			Code = "-1";
			ErrorMsg = string.Empty;
			Data = default(O);
        }
	}
}
