using Newtouch.HIS.Domain.Entity.OutpatientManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage
{
	class ChongQingMzjsDto
	{
	}
	/// <summary>
	/// 添加处方明细（交易类别代码:04）入参
	/// </summary>
	public class UploadPrescriptionsInPut
	{
		/// <summary>
		/// 住院门诊号
		/// </summary> 
		public string zymzh { get; set; }
		/// <summary>
		/// 处方明细列表
		/// </summary> 
		public IList<UploadPrescriptionsListInPut> cflist { get; set; }

	}
	public class UploadPrescriptionsListInPut
	{
		/// <summary>
		/// 处方号
		/// </summary> 
		public string cfh { get; set; }
		/// <summary>
		/// 开方日期
		/// </summary> 
		public string kfrq { get; set; }
		/// <summary>
		/// 项目医保流水号
		/// </summary> 
		public string xmyblsh { get; set; }
		/// <summary>
		/// 医院内码
		/// </summary> 
		public string yynm { get; set; }
		/// <summary>
		/// 项目名称
		/// </summary> 
		public string xmmc { get; set; }
		/// <summary>
		/// 单价
		/// </summary> 
		public decimal dj { get; set; }
		/// <summary>
		/// 数 量
		/// </summary> 
		public decimal? sl { get; set; }
		/// <summary>
		/// 急诊标志
		/// </summary> 
		public string jzbz { get; set; }
		/// <summary>
		/// 处方医师名字
		/// </summary> 
		public string cfssmc { get; set; }
		/// <summary>
		/// 经办人
		/// </summary> 
		public string jbr { get; set; }
		/// <summary>
		/// 单位
		/// </summary> 
		public string dw { get; set; }
		/// <summary>
		/// 规格
		/// </summary> 
		public string gg { get; set; }
		/// <summary>
		/// 剂型
		/// </summary> 
		public string jx { get; set; }
		/// <summary>
		/// 冲消明细流水号
		/// </summary> 
		public string cxmxlsh { get; set; }
		/// <summary>
		/// 金额
		/// </summary> 
		public decimal? je { get; set; }
		/// <summary>
		/// 科室编码
		/// </summary> 
		public string ksbm { get; set; }
		/// <summary>
		/// 科室名称
		/// </summary> 
		public string ksmc { get; set; }
		/// <summary>
		/// 医师编码
		/// </summary> 
		public string ysbm { get; set; }
		/// <summary>
		/// 每次用量
		/// </summary> 
		public decimal? mcyl { get; set; }
		/// <summary>
		/// 用法标准
		/// </summary> 
		public string yfbz { get; set; }
		/// <summary>
		/// 执行周期
		/// </summary> 
		public string zxzq { get; set; }
		/// <summary>
		/// 险种类别
		/// </summary> 
		public string xzlb { get; set; }
		/// <summary>
		/// 转自费标识
		/// </summary> 
		public string zzfbz { get; set; }
		/// <summary>
		/// 单次用药剂量
		/// </summary> 
		public decimal? dcyyjl { get; set; }
		/// <summary>
		/// 单次用药剂量单位
		/// </summary> 
		public string dcyyjldw { get; set; }
		/// <summary>
		/// 单次用量
		/// </summary> 
		public decimal? dcyl { get; set; }
		/// <summary>
		/// 最小计量单位
		/// </summary> 
		public string zxjldw { get; set; }
		/// <summary>
		/// 取药总量
		/// </summary> 
		public decimal?  qyzl { get; set; }
		/// <summary>
		/// 用药途径
		/// </summary> 
		public string yytj { get; set; }
		/// <summary>
		/// 使用频次
		/// </summary> 
		public string sypc { get; set; }
		/// <summary>
		/// 审核 ID
		/// </summary> 
		public string shid { get; set; }
		/// <summary>
		/// 用药天数
		/// </summary> 
		public string yyts { get; set; }
		/// <summary>
		/// 用药疗程
		/// </summary> 
		public string yylc { get; set; }
        /// <summary>
        /// 国家目录代码
        /// </summary>
        public string gjmldm { get; set; }
        /// <summary>
        /// 国家医师编码
        /// </summary>
        public string gjysbm { get; set; }
	}
    public class PreSettInPut
	{
		/// <summary>
		/// 住院(门诊)号
		/// </summary> 
		public string zymzh { get; set; }
		/// <summary>
		/// 截止日期
		/// </summary> 
		public string jzrq { get; set; }
		/// <summary>
		/// 住院床日
		/// </summary> 
		public string zycr { get; set; }
		/// <summary>
		/// 本次结算总金额
		/// </summary> 
		public string bcjszje { get; set; }
		/// <summary>
		/// 账户余额支付标志
		/// </summary> 
		public string zhyezfbz { get; set; }
		/// <summary>
		/// 本次结算明细总条数
		/// </summary> 
		public string bcjsmxzts { get; set; }
		/// <summary>
		/// 险种类别
		/// </summary> 
		public string xzlb { get; set; }
		/// <summary>
		/// 工伤认定编号
		/// </summary> 
		public string gsrdbh { get; set; }
		/// <summary>
		/// 工伤认定疾病编码
		/// </summary> 
		public string gsrdjbbm { get; set; }
		/// <summary>
		/// 尘肺结算类型
		/// </summary> 
		public string cfjslx { get; set; }
		/// <summary>
		/// 出院科室编码
		/// </summary> 
		public string cyksbm { get; set; }
		/// <summary>
		/// 出院医师编码
		/// </summary> 
		public string cyysbm { get; set; }
	}

	/// <summary>
	/// 更新就诊信息 (交易类别代码:03)入参
	/// </summary>
	public class MedicalUpdateInPut
	{
		/// <summary>
		/// 住院号(门诊号)
		/// </summary> 
		public string zymzh { get; set; }
		/// <summary>
		/// 更新标志
		/// </summary> 
		public string gxbz { get; set; }
		/// <summary>
		/// 医疗类别
		/// </summary> 
		public string yllb { get; set; }
		/// <summary>
		/// 入院科室编码
		/// </summary> 
		public string ryksbm { get; set; }
		/// <summary>
		/// 入院医师编码
		/// </summary> 
		public string ryysbm { get; set; }
		/// <summary>
		/// 入院日期
		/// </summary> 
		public string ryrq { get; set; }
		/// <summary>
		/// 入院诊断
		/// </summary> 
		public string ryzd { get; set; }
		/// <summary>
		/// 出院日期
		/// </summary> 
		public string cyrq { get; set; }
		/// <summary>
		/// 确诊疾病编码
		/// </summary> 
		public string qzjbbm { get; set; }
		/// <summary>
		/// 出院原因
		/// </summary> 
		public string cyyy { get; set; }
		/// <summary>
		/// 经办人
		/// </summary> 
		public string jbr { get; set; }
		/// <summary>
		/// 并发症
		/// </summary> 
		public string bfz { get; set; }
		/// <summary>
		/// 病案号
		/// </summary> 
		public string bah { get; set; }
		/// <summary>
		/// 生育证号码
		/// </summary> 
		public string syzhm { get; set; }
		/// <summary>
		/// 新生儿出生日期
		/// </summary> 
		public DateTime? xsrcsrq { get; set; }
		/// <summary>
		/// 居民特殊就诊标记
		/// </summary> 
		public string jmtsjzbj { get; set; }
		/// <summary>
		/// 险种类别
		/// </summary> 
		public string xzlb { get; set; }
		/// <summary>
		/// 转入医院编码
		/// </summary> 
		public string zryybm { get; set; }
		/// <summary>
		/// 出院诊断医生编码
		/// </summary> 
		public string cyzdysbm { get; set; }
		/// <summary>
		/// 主要病情描述
		/// </summary> 
		public string zybqms { get; set; }
		/// <summary>
		/// 出院次要疾病诊断编码 1
		/// </summary> 
		public string cycyjbzdbm1 { get; set; }
		/// <summary>
		/// 出院次要疾病诊断编码 2
		/// </summary> 
		public string cycyjbzdbm2 { get; set; }
		/// <summary>
		/// 出院次要疾病诊断编码 3
		/// </summary> 
		public string cycyjbzdbm3 { get; set; }
		/// <summary>
		/// 出院次要疾病诊断编码 4
		/// </summary> 
		public string cycyjbzdbm4 { get; set; }
		/// <summary>
		/// 出院次要疾病诊断编码 5
		/// </summary> 
		public string cycyjbzdbm5 { get; set; }
		/// <summary>
		/// 出院次要疾病诊断编码 6
		/// </summary> 
		public string cycyjbzdbm6 { get; set; }
		/// <summary>
		/// 出院次要疾病诊断编码 7
		/// </summary> 
		public string cycyjbzdbm7 { get; set; }
		/// <summary>
		/// 出院次要疾病诊断编码 8
		/// </summary> 
		public string cycyjbzdbm8 { get; set; }
		/// <summary>
		/// 出院次要疾病诊断编码 9
		/// </summary> 
		public string cycyjbzdbm9 { get; set; }
		/// <summary>
		/// 出院次要疾病诊断编码 10
		/// </summary> 
		public string cycyjbzdbm10 { get; set; }
		/// <summary>
		/// 主诉
		/// </summary> 
		public string zs { get; set; }
		/// <summary>
		/// 症状描述
		/// </summary> 
		public string zzms { get; set; }
        /// <summary>
        /// 入院国家医师编码
        /// </summary>
        public string rygjysbm { get; set; }
        /// <summary>
        /// 出院诊断国家医师编码
        /// </summary>
        public string cyzdgjysbm { get; set; }
       
    }
	/// <summary>
	/// 合并重庆，贵安，常熟医保返回字段
	/// </summary>
	public class CQMzjs05Dto: OutpatientSettGAYbFeeRelatedDTO
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
        public string pch { get; set; }
        public string bzbm { get; set; }
        public string bzmc { get; set; }
        public string yllb { get; set; }
        public string jzlx { get; set; }
        public string jzpzm { get; set; }
        /// <summary>
        /// 是否是空对象
        /// </summary>
        /// <returns></returns>
        public bool IsNull()
		{
			return prm_yka055 == 0 && string.IsNullOrWhiteSpace(prm_aac002) && !ZFY.HasValue && !XJZF.HasValue &&
			       string.IsNullOrEmpty(jylsh);
		}
	}

	public class ZYToYBDto
	{
		public string sfzh { get; set; }
		public string kh { get; set; }
		public string cblb { get; set; }
        public string ryfs { get; set; }
        public string jzid { get; set; }
        public string jzpzlx { get; set; }
        public string xzlx { get; set; }
        public string grbh { get; set; }
        public string cbdbm { get; set; }
        public string jzh { get; set; }
        public string ybver { get; set; }

    }
	/// <summary>
	/// 处方退方 (交易类别代码：10)入参
	/// </summary>
	public class RefundPrescriptionsInPut
	{
		/// <summary>
		/// 住院(门诊)号
		/// </summary>
		public string zymzh { get; set; }
		/// <summary>
		/// 处方号
		/// </summary>
		public string cfh { get; set; }
		/// <summary>
		/// 险种类别
		/// </summary>
		public string xzlb { get; set; }
	}
    public class Input_2205
    {
        public string cfh { get; set; }
        public string pch { get; set; }
    }

    public class Input_2204
    {
        public decimal? je { get; set; }
        public int? issc { get; set; }
    }

    public class Input_2203A
    {
        public string mdtrt_id { get; set; }
        public string psn_no { get; set; }
        public string med_type { get; set; }
        public string birctrl_type { get; set; }
        public string birctrl_matn_date { get; set; }
        public string matn_type { get; set; }
        public string dise_codg { get; set; }
        public string dise_name { get; set; }
    }
    public class Input_Bbrxx
    {
        /// <summary>
        /// 医疗类别
        /// </summary>
        public string med_type { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string hisId { get; set; }
        /// <summary>
        /// 就诊ID
        /// </summary>
        public string mdtrt_id { get; set; }
        /// <summary>
        /// 参保地编码
        /// </summary>
        public string insuplc_admdvs { get; set; }
        /// <summary>
        /// 人员类别
        /// </summary>
        public string psn_no { get; set; }
        public string operatorId { get; set; }
        public string operatorName { get; set; }
        /// <summary>
        /// 01 电子凭证令牌 02 身份证号 03 社保卡号
        /// </summary>
        public string mdtrt_cert_type { get; set; }
        public string mdtrt_cert_no { get; set; }
        /// <summary>
        /// 险种类型
        /// </summary>
        public string insutype { get; set; }
        public string dise_codg { get; set; }
        public string dise_name { get; set; }
        public string orgId { get; set; }
    }
    public class CancelSettInfo {
        public string mdtrt_id { get; set; }
        public string setl_id { get; set; }
        public string psn_no { get; set; }
        /// <summary>
        /// 参保地编码
        /// </summary>
        public string insuplc_admdvs { get; set; }
        public string insutype { get; set; }
        public string operatorId { get; set; }
        public string operatorName { get; set; }
        public string hisId { get; set; }
    }
}
