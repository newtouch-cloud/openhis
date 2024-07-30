using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.SelfService
{
	public class queryAppCardInfoReqDTO
	{
		/// <summary>
		/// 组织机构代码
		/// </summary>
		public string OrganizeId { get; set; }
		/// <summary>
		/// 卡号
		/// </summary>

		public string CARD_NO { get; set; }
    }

	
}
