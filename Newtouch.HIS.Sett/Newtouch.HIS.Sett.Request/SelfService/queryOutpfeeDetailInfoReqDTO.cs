using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.SelfService
{
	public class queryOutpfeeDetailInfoReqDTO
	{
		/// <summary>
		/// 组织机构代码
		/// </summary>
		public string OrganizeId { get; set; }
		/// <summary>
		/// 票据号(结算内码)
		/// </summary>
		public string RECEIPT_NO { get; set; }
	}
}
