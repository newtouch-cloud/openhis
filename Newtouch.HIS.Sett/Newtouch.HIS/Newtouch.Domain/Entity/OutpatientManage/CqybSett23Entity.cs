using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.OutpatientManage
{
	/// <summary>
	/// 重庆医保账户抵用落地
	/// </summary>
	[Table("cqyb_OutPut23")]
	public class CqybSett23Entity : IEntity<CqybSett23Entity>
	{
		/// <summary>
		/// 主键ID
		/// </summary>
		[Key]
		public int Id { get; set; }
		/// <summary>
		/// 组织机构代码
		/// </summary>
		public string OrganizeId { get; set; }
		/// <summary>
		/// 结算内码
		/// </summary>
		public int jsnm { get; set; }
		/// <summary>
		/// 门诊住院号
		/// </summary> 
		public string zymzh { get; set; }
        /// <summary>
        /// 本次抵用金额
        /// </summary>
        public decimal? bcdyje { get; set; }
        /// <summary>
        /// 抵用人姓名
        /// </summary>
        public string dyrxm { get; set; }
        /// <summary>
        /// 抵用人身份证号
        /// </summary>
        public string dyrsfzh { get; set; }
        /// <summary>
        /// 抵用人账户余额
        /// </summary>
        public decimal? dyrzhye { get; set; }
        /// <summary>
        /// 抵用交易流水号
        /// </summary>
        public string dyjylsh { get; set; }
        /// <summary>
        /// 已抵用总金额
        /// </summary>
        public decimal? ydyzje { get; set; }
        /// <summary>
        /// 结算类型 0挂号 1门诊 2 住院
        /// </summary>
        public string jslx { get; set; }
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
