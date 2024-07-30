using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
	/// <summary>
	/// 医保材料目录表
	/// </summary>
	public class G_yb_clxxVO
	{
		/// <summary>
		/// 耗材代码
		/// </summary>
		public string hcdm { get; set; }
		/// <summary>
		/// 一级分类
		/// </summary>
		public string yjfl { get; set; }
		/// <summary>
		/// 二级分类
		/// </summary>
		public string ejfl { get; set; }
		/// <summary>
		/// 三级分类
		/// </summary>
		public string sjfl { get; set; }
		/// <summary>
		/// 医保通用名
		/// </summary>
		public string ybtym { get; set; }
		/// <summary>
		/// 材质
		/// </summary>
		public string cz { get; set; }
		/// <summary>
		/// 特征
		/// </summary>
		public string tz { get; set; }
		/// <summary>
		/// 注册备案号
		/// </summary>
		public string zcbah { get; set; }
		/// <summary>
		/// 单件产品名称
		/// </summary>
		public string djcpmc { get; set; }
		/// <summary>
		/// 耗材企业
		/// </summary>
		public string hcqy { get; set; }
		/// <summary>
		/// 规格型号数
		/// </summary>
		public double ggxhs { get; set; }
		/// <summary>
		/// 注册备案人
		/// </summary>
		public string zcbar { get; set; }
		/// <summary>
		/// 拼音码
		/// </summary>
		public string pym { get; set; }
		/// <summary>
		/// 医保性质
		/// </summary>
		public string ybxz { get; set; }

	}
}
