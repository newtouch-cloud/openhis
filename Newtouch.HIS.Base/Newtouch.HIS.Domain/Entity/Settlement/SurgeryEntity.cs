using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.Settlement
{
	[Table("xt_ssm")]
	public class SurgeryEntity : IEntity<SurgeryEntity>
	{

		/// <summary>
		/// 
		/// </summary>
		[Key]
		public int id { get; set; }

		/// <summary>
		/// 手术码
		/// </summary>
		public string ssm { get; set; }

		/// <summary>
		/// 手术名称
		/// </summary>
		public string ssmc { get; set; }

		/// <summary>
		/// 手术级别
		/// </summary>
		public int ssjb { get; set; }

		/// <summary>
		/// 手术类型
		/// </summary>
		public string sslx { get; set; }

		/// <summary>
		/// 手术类型名称
		/// </summary>
		public string sslxmc { get; set; }

		/// <summary>
		/// 拼音码
		/// </summary>
		public string pym { get; set; }

		public bool zt { get; set; }

		/// <summary>
		/// 医保手术码
		/// </summary>
		public string ssm_yb { get; set; }

		/// <summary>
		/// 医保手术名称
		/// </summary>
		public string ssmc_yb { get; set; }
	}
}
