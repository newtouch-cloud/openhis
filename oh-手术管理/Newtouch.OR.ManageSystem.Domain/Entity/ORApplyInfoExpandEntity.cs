using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.Entity
{
	[Table("OR_ApplyInfo_Expand")]
	public class ORApplyInfoExpandEntity : IEntity<ORApplyInfoExpandEntity>
	{
		/// <summary>
		/// OrganizeId
		/// </summary>
		/// <returns></returns>
		public string OrganizeId { get; set; }

		/// <summary>
		/// 主键
		/// </summary>
		/// <returns></returns>
		[Key]
		public string Id { get; set; }

		/// <summary>
		/// 申请编码
		/// </summary>
		/// <returns></returns>
		public string Applyno { get; set; }

		/// <summary>
		/// 住院号
		/// </summary>
		/// <returns></returns>
		public string zyh { get; set; }

		/// <summary>
		/// 手术名称
		/// </summary>
		public string ssmc { get; set; }

		/// <summary>
		/// 手术代码
		/// </summary>
		public string ssdm { get; set; }

		/// <summary>
		/// 序号
		/// </summary>
		public int px { get; set; }

		/// <summary>
		/// 状态
		/// </summary>
		public string zt { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 创建人
		/// </summary>
		public string CreatorCode { get; set; }

		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? LastModifyTime { get; set; }
		
		/// <summary>
		/// 修改人
		/// </summary>
		public string LastModifierCode { get; set; }
	}
}
