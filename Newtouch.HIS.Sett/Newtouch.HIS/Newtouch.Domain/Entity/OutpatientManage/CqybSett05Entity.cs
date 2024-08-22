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
	/// 重庆医保结算返回落地
	/// </summary>
	[Table("cqyb_OutPut05")]
	public class CqybSett05Entity : IEntity<CqybSett05Entity>
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
		/// 交易流水号
		/// </summary> 
		public string jylsh { get; set; }
		/// <summary>
		/// 类别1：门诊结算，2门诊结算红冲 3：住院结算，4住院结算红冲
		/// </summary>
		public string jslb { get; set; }
		/// <summary>
		/// 统筹支付
		/// </summary> 
		public decimal? cqtczf { get; set; }
		/// <summary>
		/// 帐户支付
		/// </summary> 
		public decimal? zhzf { get; set; }
		/// <summary>
		/// 公务员补助
		/// </summary> 
		public decimal? gwybz { get; set; }
		/// <summary>
		/// 现金支付
		/// </summary> 
		public decimal? cqxjzf { get; set; }
		/// <summary>
		/// 大额理赔金额
		/// </summary> 
		public decimal? delpje { get; set; }
		/// <summary>
		/// 历史起付线公务员返还
		/// </summary> 
		public decimal? lsqfxgwyff { get; set; }
		/// <summary>
		/// 帐户余额
		/// </summary> 
		public decimal? zhye { get; set; }
		/// <summary>
		/// 单病种定点医疗机构垫支
		/// </summary> 
		public decimal? dbzddyljgdz { get; set; }
		/// <summary>
		/// 民政救助金额
		/// </summary> 
		public decimal? mzjzje { get; set; }
		/// <summary>
		/// 民政救助门诊余额
		/// </summary> 
		public decimal? mzjzmzye { get; set; }
		/// <summary>
		/// 耐多药项目支付金额
		/// </summary> 
		public decimal? ndyxmzfje { get; set; }
		/// <summary>
		/// 一般诊疗支付数
		/// </summary> 
		public decimal? ybzlzfs { get; set; }
		/// <summary>
		/// 神华救助基金支付数
		/// </summary> 
		public decimal? shjzjjzfs { get; set; }
		/// <summary>
		/// 本年统筹支付累计
		/// </summary> 
		public decimal? bntczflj { get; set; }
		/// <summary>
		/// 本年大额支付累计
		/// </summary> 
		public decimal? bndezflj { get; set; }
		/// <summary>
		/// 特病起付线支付累计
		/// </summary> 
		public decimal? tbqfxzflj { get; set; }
		/// <summary>
		/// 耐多药项目累计
		/// </summary> 
		public decimal? ndyxmlj { get; set; }
		/// <summary>
		/// 本年民政救助住院支付累计
		/// </summary> 
		public decimal? bnmzjzzyzflj { get; set; }
		/// <summary>
		/// 中心结算时间
		/// </summary> 
		public DateTime? zxjssj { get; set; }
		/// <summary>
		/// 本次起付线支付金额
		/// </summary> 
		public decimal? bcqfxzfje { get; set; }
		/// <summary>
		/// 本次进入医保范围费用
		/// </summary> 
		public decimal? bcjrybfwfy { get; set; }
		/// <summary>
		/// 药事服务支付数
		/// </summary> 
		public decimal? ysfwzfs { get; set; }
		/// <summary>
		/// 医院超标扣款金额
		/// </summary> 
		public decimal? yycbkkje { get; set; }
		/// <summary>
		/// 生育基金支付
		/// </summary> 
		public decimal? syjjzf { get; set; }
		/// <summary>
		/// 生育现金支付
		/// </summary> 
		public decimal? syxjzf { get; set; }
		/// <summary>
		/// 工伤基金支付
		/// </summary> 
		public decimal? gsjjzf { get; set; }
		/// <summary>
		/// 工伤现金支付
		/// </summary> 
		public decimal? gsxjzf { get; set; }
		/// <summary>
		/// 工伤单病种机构垫支
		/// </summary> 
		public decimal? gsdbzjgdz { get; set; }
		/// <summary>
		/// 工伤全自费原因
		/// </summary> 
		public string gsqzfyy { get; set; }
		/// <summary>
		/// 其他补助
		/// </summary> 
		public decimal? qtbz { get; set; }
		/// <summary>
		/// 生育账户支付
		/// </summary> 
		public decimal? syzhzf { get; set; }
		/// <summary>
		/// 工伤账户支付
		/// </summary> 
		public decimal? gszhzf { get; set; }
		/// <summary>
		/// 本次结算是否健康扶贫人员
		/// </summary> 
		public string bcjssfjkfpry { get; set; }
		/// <summary>
		/// 健康扶贫医疗基金
		/// </summary> 
		public decimal? jkfpyljj { get; set; }
		/// <summary>
		/// 精准脱贫保险金额
		/// </summary> 
		public decimal? jztpbxje { get; set; }
		/// <summary>
		/// 中盖基金支付 （退费时所有字段）
		/// </summary>
		public decimal? zgjjzf { get; set; }
		/// <summary>
		/// 降消项目金额 （退费时所有字段）
		/// </summary>
		public decimal? jxxmje { get; set; }
		/// <summary>
		/// 应退支付扩展预留 2（退费时所有字段）
		/// </summary>
		public decimal? zfkzyl2 { get; set; }
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
        /// 其他扶贫报销金额
        /// </summary> 
        public decimal? qtfpbxje { get; set; }
        public string pch { get; set; }
        /// <summary>
        /// 病种编码
        /// </summary>
        public string bzbm { get; set; }
        public string bzmc { get; set; }
        public string yllb { get; set; }
        public string jzlx { get; set; }
        public string jzpzm { get; set; }
    }
}
