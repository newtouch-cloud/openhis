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
	[Table("cqyb_OutPutInPar04")]
	public class CqybUploadInPres04Entity : IEntity<CqybUploadInPres04Entity>
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
		/// 处方号
		/// </summary>
		public string cfh { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string pch { get; set; }
		/// <summary>
		/// 上传类型1门诊，2住院
		/// </summary>
		public string jytype { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal? je { get; set; }
        /// <summary>
		/// 审批标志 0未审批 1审批
		/// </summary>
		public string spbz { get; set; }
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
        /// 国家目录代码
        /// </summary>
        public string gjmldm { get; set; }
        /// <summary>
        /// 国家医师编码
        /// </summary>
        public string gjysbm { get; set; }
        /// <summary>
        /// 医师编码(证件号)
        /// </summary>
        public string ysbm { get; set; }
    }
}
