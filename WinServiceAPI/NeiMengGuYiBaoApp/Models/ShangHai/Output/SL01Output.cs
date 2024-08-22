using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Output
{
	public class SL01Output: OutputBase
	{
		/// <summary>
		/// 返回对账结果
		/// </summary>
		public string resultcollate { get; set; }

		/// <summary>
		/// 返回对账日
		/// </summary>
		public string daycollate { get; set; }

	}
}
