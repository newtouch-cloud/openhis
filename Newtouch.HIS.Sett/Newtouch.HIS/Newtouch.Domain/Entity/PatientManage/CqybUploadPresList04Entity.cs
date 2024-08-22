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
	[Table("cqyb_OutPut04")]
	public class CqybUploadPresList04Entity : IEntity<CqybUploadPresList04Entity>
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
		/// 住院门诊号
		/// </summary>
		public string zymzh { get; set; }
		/// <summary>
		/// 交易流水号
		/// </summary> 
		public string jylsh { get; set; }
		/// <summary>
		/// 上传类型1门诊，2住院
		/// </summary>
		public string jytype { get; set; }
		/// <summary>
		/// |项目单价
		/// </summary> 
		public string dj { get; set; }
		/// <summary>
		/// 审批标记
		/// </summary> 
		public string spbz { get; set; }
		/// <summary>
		/// 审批规则
		/// </summary> 
		public string spgz { get; set; }
		/// <summary>
		/// 项目费用总额
		/// </summary> 
		public string xmfyze { get; set; }
		/// <summary>
		/// 项目等级
		/// </summary> 
		public string xmdj { get; set; }
		/// <summary>
		/// 自付比例
		/// </summary> 
		public string zfbl { get; set; }
		/// <summary>
		/// 标准单价
		/// </summary> 
		public string bzdj { get; set; }
		/// <summary>
		/// 自付金额
		/// </summary> 
		public string zfje { get; set; }
		/// <summary>
		/// 自费金额
		/// </summary> 
		public string zfzje { get; set; }
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
        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }
        /// <summary>
        /// 医院内码
        /// </summary>
        public string yynm { get; set; }
    }
}
