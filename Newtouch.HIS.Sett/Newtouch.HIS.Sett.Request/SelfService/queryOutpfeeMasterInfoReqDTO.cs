using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.SelfService
{
	public class queryOutpfeeMasterInfoReqDTO : queryAppCardInfoReqDTO
	{
		/// <summary>
		/// 开始时间
		/// </summary>
		public string START_DATE { get; set; }
		/// <summary>
		/// 结束时间
		/// </summary>

		public string END_DATE { get; set; }
	}
}
