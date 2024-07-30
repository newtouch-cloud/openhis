using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.SelfService
{
	public class queryRegistDeptMarkReqDTO
	{
		/// <summary>
		/// 看诊时间
		/// </summary>
		public string VisitDate { get; set; }
		/// <summary>
		/// 组织机构代码
		/// </summary>
		public string OrganizeId { get; set; }
    }
}
