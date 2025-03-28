using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.PatientManage
{
	/// <summary>
	/// 重庆医保_就诊登记02返回落地
	/// </summary>
	[Table("cqyb_OutPut02")]
	public class CqybMedicalReg02Entity : IEntity<CqybMedicalReg02Entity>
	{
		/// <summary>
		/// 主键ID
		/// </summary>
		[Key]
		public int Id { get; set; }
		/// <summary>
		/// 组织机构Id（有具体业务的医院）
		/// </summary>
		public string OrganizeId { get; set; }
		/// <summary>
		/// 患者id
		/// </summary>
		public string patid { get; set; }
		/// <summary>
		/// 门诊住院号
		/// </summary>
		public string zymzh { get; set; }
		/// <summary>
		/// 交易流水号
		/// </summary>
		public string jylsh { get; set; }
		/// <summary>
		/// 帐户余额
		/// </summary>
		public string zhye { get; set; }
		/// <summary>
		/// 原转出医院名称
		/// </summary>
		public string yzcyymc { get; set; }
		/// <summary>
		/// 参保类别
		/// </summary>
		public string cblx { get; set; }
		/// <summary>
		/// 登记类型1门诊，2住院
		/// </summary>
		public string jytype { get; set; }
		/// <summary>
		/// 创建用户ID
		/// </summary>
		public string CreatorCode { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 最后修改时间
		/// </summary>
		public DateTime? LastModifyTime { get; set; }
		/// <summary>
		/// 最后修改用户ID
		/// </summary>
		public string LastModifierCode { get; set; }
		/// <summary>
		/// 排序
		/// </summary>
		public int px { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		public string zt { get; set; }

	}
}
