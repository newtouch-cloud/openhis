using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.InputDto
{
	class ChongQingZyjsDto
	{
	}
	public class CQZyjs05Dto : InpatientSettGAYbFeeRelatedDTO
	{
		/// <summary>
		/// 交易流水号
		/// </summary> 
		public string jylsh { get; set; }
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
		public DateTime zxjssj { get; set; }
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
	}
}
