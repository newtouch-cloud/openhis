using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.DTO.OutputDto
{
	/// <summary>
	/// 系统手术字典
	/// </summary>
	public class SysOpListDto
	{
		/// <summary>
		/// 手术代码
		/// </summary>
		public string ssdm { get; set; }
		/// <summary>
		/// 手术名称
		/// </summary>
		public string ssmc { get; set; }
		/// <summary>
		/// 手术助记码
		/// </summary>
		public string zjm { get; set; }
		/// <summary>
		/// 手术级别
		/// </summary>
		public string ssjb { get; set; }
	}
}
