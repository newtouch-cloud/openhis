using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
	public class SelfServiceVO
	{
	}

	public class zzjRegScheduleVO
	{
		/// <summary>
		/// 科室
		/// </summary>
		public string ks { get; set; }
		/// <summary>
		/// 科室名称
		/// </summary>
		public string ksmc { get; set; }
		/// <summary>
		/// 排班id
		/// </summary>
		public string ghpbId { get; set; }
		/// <summary>
		/// 门急诊标志（1门诊 2急诊 3专家）
		/// </summary>
		public string mjzbz { get; set; }
	}

	public class zzjCardInfoVO
	{
		/// <summary>
		/// 身份证号
		/// </summary>
		public string ID_NO { get; set; }
		/// <summary>
		/// 就诊卡号
		/// </summary>
		public string CARD_NO { get; set; }
		/// <summary>
		/// 病人ID
		/// </summary>
		public string PATIENT_ID { get; set; }
		/// <summary>
		/// 病人姓名
		/// </summary>
		public string PATIENT_NAME { get; set; }
		/// <summary>
		/// 就诊卡类型
		/// </summary>
		public string CARD_TYPE { get; set; }
		/// <summary>
		/// 就诊卡类型名称
		/// </summary>
		public string CARD_TYPENAME { get; set; }
		/// <summary>
		/// 手机号
		/// </summary>
		public string PHONE_NUMBER { get; set; }
		/// <summary>
		/// 就诊卡状态
		/// </summary>
		public string STATUS { get; set; }
		/// <summary>
		/// 就诊卡余额
		/// </summary>
		public string TOTAL_FEE { get; set; }
		/// <summary>
		/// 查询时间
		/// </summary>
		public string QUERY_TIME { get; set; }
	}

	public class OutpfeeMasterInfoVO: OutpfeeMasterPatInfo
	{
		/// <summary>
		/// 结算列表
		/// </summary>
		public List<OutpfeeMasterInfoList> ITEMS { get; set; }
		
	}
	public class OutpfeeMasterPatInfo
	{
		/// <summary>
		/// 患者姓名
		/// </summary>
		public string PATIENT_NAME { get; set; }
		/// <summary>
		/// 患者编码
		/// </summary>
		public string PATIENT_ID { get; set; }
		/// <summary>
		/// 身份证号
		/// </summary>
		public string ID_NO { get; set; }
		/// <summary>
		/// 电话号码
		/// </summary>
		public string PHONE_NUMBER { get; set; }
		/// <summary>
		/// 性别
		/// </summary>
		public string GENDER { get; set; }
		/// <summary>
		/// 年龄
		/// </summary>
		public string AGE { get; set; }
		/// <summary>
		/// 出生日期
		/// </summary>
		public string BIRTHDAY { get; set; }

	}
	public class OutpfeeMasterInfoList
	{
		/// <summary>
		/// 就诊日期
		/// </summary>
		public string VISIT_DATE { get; set; }
		/// <summary>
		/// 就诊序号
		/// </summary>
		public string VISIT_NO { get; set; }
		/// <summary>
		/// 就诊科室
		/// </summary>
		public string DEPT_NAME { get; set; }
		/// <summary>
		/// 就诊卡号
		/// </summary>
		public string CARD_NO { get; set; }
		/// <summary>
		/// 门诊号
		/// </summary>
		public string OUTPATIENT_NO { get; set; }
		/// <summary>
		/// 票据号
		/// </summary>
		public string RECEIPT_NO { get; set; }
		/// <summary>
		/// 业务流水号
		/// </summary>
		public string BUSSINESS_NO { get; set; }
		/// <summary>
		/// 发票号
		/// </summary>
		public string INVOICE_NO { get; set; }
		/// <summary>
		/// 发票类型
		/// </summary>
		public string INVOICE_TYPE { get; set; }
		/// <summary>
		/// 费用类别
		/// </summary>
		public string INSUR_TYPE { get; set; }
		/// <summary>
		/// 报销卡类别
		/// </summary>
		public string CARD_TYPE { get; set; }
		/// <summary>
		/// 报销卡卡号
		/// </summary>
		public string INSUR_CARD_NO { get; set; }
		/// <summary>
		/// 报销卡交易流水号
		/// </summary>
		public string INSUR_RCPT_NO { get; set; }
		/// <summary>
		/// 总金额
		/// </summary>
		public string ACCOUNT_SUM { get; set; }
		/// <summary>
		/// 统筹支付金额
		/// </summary>
		public string INSUR_PAY { get; set; }
		/// <summary>
		/// 账户支付金额
		/// </summary>
		public string ACCOUNT_PAY { get; set; }
		/// <summary>
		/// 个人现金支付金额
		/// </summary>
		public string PERSONAL_PAY { get; set; }
		/// <summary>
		/// 大病基金
		/// </summary>
		public string DISEASE_FUND { get; set; }
		/// <summary>
		/// 公务员补助
		/// </summary>
		public string INJURY_FUND { get; set; }
		/// <summary>
		/// 生育基金
		/// </summary>
		public string FERTILITY_FUND { get; set; }
		/// <summary>
		/// 自费金额
		/// </summary>
		public string MYSELF_PAY { get; set; }
		/// <summary>
		/// 分类自负金额
		/// </summary>
		public string CLASS_SELF_PAY { get; set; }
		/// <summary>
		/// 账户余额
		/// </summary>
		public string INSUR_ACCOUNT { get; set; }
		/// <summary>
		/// 结算日期
		/// </summary>
		public string SETTLEMENT_DATE { get; set; }
		/// <summary>
		/// 订单号
		/// </summary>
		public string ORDER_NO { get; set; }
		/// <summary>
		/// 支付渠道
		/// </summary>
		public string PAY_CHANNEL { get; set; }
	}

	public class OutpfeeDetailVO: OutpfeeDetailPatInfo
	{
		public IList<OutpfeeDetailList> ITEMS { get; set; }
    }

	public class OutpfeeDetailPatInfo
	{
		/// <summary>
		/// 患者姓名
		/// </summary>
		public string PATIENT_NAME { get; set; }
		/// <summary>
		/// 患者编码
		/// </summary>
		public string PATIENT_ID { get; set; }
		/// <summary>
		/// 身份证号
		/// </summary>
		public string ID_NO { get; set; }
		/// <summary>
		/// 电话号码
		/// </summary>
		public string PHONE_NUMBER { get; set; }
		/// <summary>
		/// 就诊日期
		/// </summary>
		public string VISIT_DATE { get; set; }
		/// <summary>
		/// 就诊序号
		/// </summary>
		public string VISIT_NO { get; set; }
		/// <summary>
		/// 支付时间
		/// </summary>
		public string PAY_DATE { get; set; }
	}

	public class OutpfeeDetailList
	{
		/// <summary>
		/// 此费用唯一标识
		/// </summary>
		public string M_FEIYONGID { get; set; }
		/// <summary>
		/// 项目序号
		/// </summary>
		public string ITEM_NO { get; set; }
		/// <summary>
		/// 项目编码
		/// </summary>
		public string ITEM_CODE { get; set; }
		/// <summary>
		/// 项目名称
		/// </summary>
		public string ITEM_NAME { get; set; }
		/// <summary>
		/// 项目规格
		/// </summary>
		public string ITEM_SPEC { get; set; }
		/// <summary>
		/// 项目单位
		/// </summary>
		public string ITEM_UNITS { get; set; }
		/// <summary>
		/// 项目剂型
		/// </summary>
		public string ITEM_FORM { get; set; }
		/// <summary>
		/// 项目数量
		/// </summary>
		public string ITEM_AMOUNT { get; set; }
		/// <summary>
		/// 项目单价
		/// </summary>
		public string ITEM_PRICE { get; set; }
		/// <summary>
		/// 项目总金额
		/// </summary>
		public string ITEM_COSTS { get; set; }
		/// <summary>
		/// 项目类别
		/// </summary>
		public string ITEM_CLASS { get; set; }
		/// <summary>
		/// 医保类别
		/// </summary>
		public string INSUR_CLASS { get; set; }
		/// <summary>
		/// 自负比例
		/// </summary>
		public string MYSELF_SCALE { get; set; }
		/// <summary>
		/// 自费金额
		/// </summary>
		public string SELF_PAY { get; set; }
		/// <summary>
		/// 分类自负金额
		/// </summary>
		public string CLASS_SELF_PAY { get; set; }
		/// <summary>
		/// 医保项目编码
		/// </summary>
		public string MEDICAL_INSURANCE_ITEM_CODE { get; set; }
		/// <summary>
		/// 医保项目名称
		/// </summary>
		public string MEDICAL_INSURANCE_ITEM_NAME { get; set; }
		/// <summary>
		/// 开单医生
		/// </summary>
		public string DOCTOR_NAME { get; set; }
		/// <summary>
		/// 执行科室
		/// </summary>
		public string PERFORM_DEPT { get; set; }
	}

	public class InPatientInfoVO : InPatientInfoPatInfo
	{
		public IList<InPatientInfoList> ITEMS { get; set; }
	}

	public class InPatientInfoPatInfo
	{
		/// <summary>
		/// 患者编码
		/// </summary>
		public string PATIENT_ID { get; set; }
		/// <summary>
		/// 身份证号
		/// </summary>
		public string ID_NO { get; set; }
		/// <summary>
		/// 在院科室
		/// </summary>
		public string DeptName { get; set; }
		/// <summary>
		/// 在院病区
		/// </summary>
		public string WardName { get; set; }
		/// <summary>
		/// 床位号
		/// </summary>
		public string BedNo { get; set; }
		/// <summary>
		/// 在院状态
		/// </summary>
		public string PatStatus { get; set; }
		/// <summary>
		/// 入院时间
		/// </summary>
		public string InHospitalDate { get; set; }
		/// <summary>
		/// 入区时间
		/// </summary>
		public string InDate { get; set; }
		/// <summary>
		/// 出区时间
		/// </summary>
		public string OutDate { get; set; }
		/// <summary>
		/// 性别
		/// </summary>
		public string SexName { get; set; }
		/// <summary>
		/// 预交金总额
		/// </summary>
		public string AccBalance { get; set; }
		/// <summary>
		/// 总费用
		/// </summary>
		public string TotalMoney { get; set; }
		/// <summary>
		/// 住院号
		/// </summary>
		public string zyh { get; set; }
	}

	public class InPatientInfoList
	{
		/// <summary>
		/// 项目名称
		/// </summary>
		public string ITEM_NAME { get; set; }
		/// <summary>
		/// 项目剂型
		/// </summary>
		public string ITEM_FORM { get; set; }
		/// <summary>
		/// 项目数量
		/// </summary>
		public string ITEM_AMOUNT { get; set; }
		/// <summary>
		/// 项目单价
		/// </summary>
		public string ITEM_PRICE { get; set; }
		/// <summary>
		/// 项目总金额
		/// </summary>
		public string ITEM_COSTS { get; set; }
		/// <summary>
		/// 项目类别
		/// </summary>
		public string ITEM_CLASS { get; set; }
		/// <summary>
		/// 项目单位
		/// </summary>
		public string ITEM_UNITS { get; set; }
		/// <summary>
		/// 产生日期
		/// </summary>
		public string ITEM_DATE { get; set; }
	}
}
