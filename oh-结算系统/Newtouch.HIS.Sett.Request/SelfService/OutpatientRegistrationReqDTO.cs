using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.SelfService
{
	public class OutpatientRegistrationReqDTO
	{
		/// <summary>
		/// 组织机构ID
		/// </summary>
		public string OrganizeId { get; set; }
		/// <summary>
		/// 卡号
		/// </summary>
		public string kh { get; set; }
		/// <summary>
		/// 门急诊标志
		/// </summary>
		public string mjzbz { get; set; }
		/// <summary>
		/// 科室
		/// </summary>
		public string ks { get; set; }
		/// <summary>
		/// 科室名称
		/// </summary>
		public string ksmc { get; set; }
		//public string ys { get; set; }
		//public string ysmc { get; set; }
		/// <summary>
		/// 病人性质
		/// </summary>
		public string brxz { get; set; }
		/// <summary>
		/// 挂号排班id
		/// </summary>
		public string ghpbId { get; set; }
    }
}
