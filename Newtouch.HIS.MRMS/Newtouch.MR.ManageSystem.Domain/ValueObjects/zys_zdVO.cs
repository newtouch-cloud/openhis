using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Newtouch.MR.ManageSystem.Domain.ValueObjects
{
	public class zys_zdVO
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
		/// 诊断类型
		/// </summary>
		public int zdType { set; get; }
		/// <summary>
		/// 诊断描述
		/// </summary>
		public string zdms { get; set; }
		/// <summary>
		/// 治疗结果
		/// </summary>
		public string zljg { get; set; }
		/// <summary>
		/// 治疗天数
		/// </summary>
		public string zlts { get; set; }
		/// <summary>
		/// 诊断日期
		/// </summary>
		public DateTime? zdrq { get; set; }

	}
}