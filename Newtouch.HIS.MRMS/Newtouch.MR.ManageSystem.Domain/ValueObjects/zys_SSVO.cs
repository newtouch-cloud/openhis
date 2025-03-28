using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.ValueObjects
{
	public class zys_ssVO
	{

		/// <summary>
		/// id
		/// </summary>
		public string id { set; get; }
		/// <summary>
		/// 住院号
		/// </summary>
		public string zyh { set; get; }
		/// <summary>
		/// 手术名称
		/// </summary>
		public string ssmc { get; set; }
		/// <summary>
		/// 麻醉方法
		/// </summary>
		public string mzfs { get; set; }
		/// <summary>
		/// 手术医师
		/// </summary>
		public string ssys { get; set; }
		/// <summary>
		/// 手术日期
		/// </summary>
		public DateTime? ssrq { get; set; }
	}
}
