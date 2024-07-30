using System.Collections.Generic;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    public class S16ResponseDTO
    {
        /// <summary>
        /// 住院补偿序号
        /// </summary>
        public string inpId { get; set; }
        /// <summary>
        /// 个人编码
        /// </summary>
        public string memberId { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string memberName { get; set; }
        /// <summary>
        /// 患者性别
        /// </summary>
        public string memberSex { get; set; }
        /// <summary>
        /// 患者身份证号
        /// </summary>
        public string idCard { get; set; }
        /// <summary>
        /// 患者年龄
        /// </summary>
        public string memberAge { get; set; }
        /// <summary>
        /// 家庭编号
        /// </summary>
        public string familyId { get; set; }
        /// <summary>
        /// 医疗证卡号
        /// </summary>
        public string medicalNo { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string inpatientNo { get; set; }
        /// <summary>
        /// 就诊类型
        /// </summary>
        public string inpatientType { get; set; }
        /// <summary>
        /// 入院时间
        /// </summary>
        public string admissionDate { get; set; }
        /// <summary>
        /// 出院时间
        /// </summary>
        public string dischargeDate { get; set; }
        /// <summary>
        /// 实际住院天数
        /// </summary>
        public string dateNum { get; set; }
        /// <summary>
        /// 就医机构代码
        /// </summary>
        public string hospitalNo { get; set; }
        /// <summary>
        /// 就医机构名称
        /// </summary>
        public string hospitalName { get; set; }
        /// <summary>
        /// 就医机构级别
        /// </summary>
        public string hospitalLevel { get; set; }
        /// <summary>
        /// 民政性质
        /// </summary>
        public string mzPro { get; set; }
        /// <summary>
        /// 人员属性
        /// </summary>
        public string memberPro { get; set; }
        /// <summary>
        /// 人员属性
        /// </summary>
        public string memberProName { get; set; }
        /// <summary>
        /// 入院科室
        /// </summary>
        public string admissionDepartments { get; set; }
        /// <summary>
        /// 入院科室
        /// </summary>
        public string admissionDepartmentsName { get; set; }
        /// <summary>
        /// 出院科室
        /// </summary>
        public string dischargeDepartments { get; set; }
        /// <summary>
        /// 出院科室
        /// </summary>
        public string dischargeDepartmentsName { get; set; }
        /// <summary>
        /// 经治医生
        /// </summary>
        public string treatingPhysician { get; set; }
        /// <summary>
        /// 入院状态
        /// </summary>
        public string admissionStatus { get; set; }
        /// <summary>
        /// 出院状态
        /// </summary>
        public string dischargeStatus { get; set; }
        /// <summary>
        /// 疾病代码
        /// </summary>
        public string diseaseCode { get; set; }
        /// <summary>
        /// 疾病名称
        /// </summary>
        public string diseaseName { get; set; }
        /// <summary>
        /// 初步诊断
        /// </summary>
        public string initialDiagnosis { get; set; }
        /// <summary>
        /// 手术名称代码
        /// </summary>
        public string surgeryCode { get; set; }
        /// <summary>
        /// 手术名称代码
        /// </summary>
        public string surgeryName { get; set; }
        /// <summary>
        /// 病区
        /// </summary>
        public string wardArea { get; set; }
        /// <summary>
        /// 病房号
        /// </summary>
        public string wardNo { get; set; }
        /// <summary>
        /// 床位号
        /// </summary>
        public string berthNo { get; set; }
        /// <summary>
        /// 补偿类别
        /// </summary>
        public string compensateType { get; set; }
        /// <summary>
        /// 补偿类别
        /// </summary>
        public string compensateTypeName { get; set; }
        /// <summary>
        /// 补偿机构
        /// </summary>
        public string compensateMechanism { get; set; }
        /// <summary>
        /// 补偿机构名称
        /// </summary>
        public string compensateMechanismName { get; set; }
        /// <summary>
        /// 补偿日期
        /// </summary>
        public string compensateDate { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string managerCode { get; set; }
        /// <summary>
        /// 总费用
        /// </summary>
        public string totalCost { get; set; }
        /// <summary>
        /// 保内费用
        /// </summary>
        public string insuranceCost { get; set; }
        /// <summary>
        /// 起伏线
        /// </summary>
        public string undulatingLine { get; set; }
        /// <summary>
        /// 补偿费用
        /// </summary>
        public string compensateCost { get; set; }
        /// <summary>
        /// 自付金额
        /// </summary>
        public string setPayCost { get; set; }
        /// <summary>
        /// 保内自付金额
        /// </summary>
        public string insurancePayCost { get; set; }
        /// <summary>
        /// 核算金额
        /// </summary>
        public string auditCompensateCost { get; set; }
        /// <summary>
        /// 初审扣减
        /// </summary>
        public string auditDeductionCost { get; set; }
        /// <summary>
        /// 终审扣减
        /// </summary>
        public string lastAuditDeductionCost { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// 农保补偿比
        /// </summary>
        public string compensateProportion { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 证件是否齐全
        /// </summary>
        public string isIncrease { get; set; }
        /// <summary>
        /// 是否转诊
        /// </summary>
        public string isReferra { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string areaName { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string userCode { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 病人类型
        /// </summary>
        public string inpatientTypeOflocal { get; set; }
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string bankCardNo { get; set; }
        /// <summary>
        /// 是否绑定银行卡号
        /// </summary>
        public string isBankCardNo { get; set; }
        /// <summary>
        /// 银行卡号开户人姓
        /// </summary>
        public string accountHolder { get; set; }
        /// <summary>
        /// 患者与开户人关系
        /// </summary>
        public string holderRelation { get; set; }
        /// <summary>
        /// 就医机构名称备注
        /// </summary>
        public string hospitalNameRemark { get; set; }
        /// <summary>
        /// 保险金额
        /// </summary>
        public string bxAccount { get; set; }
        /// <summary>
        /// 计算公式
        /// </summary>
        public string jsgs { get; set; }
        /// <summary>
        /// 大额补偿金额
        /// </summary>
        public string bigAccountCost { get; set; }
        /// <summary>
        /// 床位包干补偿金额
        /// </summary>
        public string cwAccountCost { get; set; }
        /// <summary>
        /// 二次补偿金额
        /// </summary>
        public string secondCompensateCost { get; set; }
        /// <summary>
        /// 家庭账户抵扣
        /// </summary>
        public string accRedeem { get; set; }
        /// <summary>
        /// 累计补偿是否已达封顶线
        /// </summary>
        public string isOverLimitLine { get; set; }
        /// <summary>
        /// 是否保底
        /// </summary>
        public string isLessLimit { get; set; }
        /// <summary>
        /// 年度补偿次数
        /// </summary>
        public string inpYearCount { get; set; }
        /// <summary>
        /// 年度累计住院总费用
        /// </summary>
        public string inpTotalYearSum { get; set; }
        /// <summary>
        /// 年度补偿金额
        /// </summary>
        public string inpBcYearSum { get; set; }
        /// <summary>
        /// 自费费用总金额
        /// </summary>
        public string inpSelfPayYearSum { get; set; }
        /// <summary>
        /// 病人所属县区编号
        /// </summary>
        public string countryCode { get; set; }
        /// <summary>
        /// 病人所属县区名称
        /// </summary>
        public string countryName { get; set; }
        /// <summary>
        /// 初审时间
        /// </summary>
        public string auditDate { get; set; }
        /// <summary>
        /// 初审机构
        /// </summary>
        public string auditOrg { get; set; }
        /// <summary>
        /// 初审人员
        /// </summary>
        public string auditUser { get; set; }
        /// <summary>
        /// 初审扣减原因
        /// </summary>
        public string auditRemark { get; set; }
        /// <summary>
        /// 终审时间
        /// </summary>
        public string lastAuditDate { get; set; }
        /// <summary>
        /// 终审机构
        /// </summary>
        public string lastAuditOrg { get; set; }
        /// <summary>
        /// 终审人员
        /// </summary>
        public string lastAuditUser { get; set; }
        /// <summary>
        /// 终审扣减原因
        /// </summary>
        public string lastAuditRemark { get; set; }
        /// <summary>
        /// 补偿状态
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// 冲红人员
        /// </summary>
        public string destoryUserName { get; set; }
        /// <summary>
        /// 冲红时间
        /// </summary>
        public string destoryDate { get; set; }
        /// <summary>
        /// 终审标识状态
        /// </summary>
        public string lastAuditState { get; set; }
        /// <summary>
        /// 药品费
        /// </summary>
        public string yp { get; set; }
        /// <summary>
        /// 检查费
        /// </summary>
        public string jc { get; set; }
        /// <summary>
        /// 治疗费
        /// </summary>
        public string zl { get; set; }
        /// <summary>
        /// 床位费
        /// </summary>
        public string cw { get; set; }
        /// <summary>
        /// 材料费
        /// </summary>
        public string cl { get; set; }
        /// <summary>
        /// 其他费
        /// </summary>
        public string qt { get; set; }
        /// <summary>
        /// 民政局补偿金额
        /// </summary>
        public string mzjCompensateCost { get; set; }
        /// <summary>
        /// 计生补偿金额
        /// </summary>
        public string jsCompensateCost { get; set; }
        /// <summary>
        /// 大病保险补偿金额
        /// </summary>
        public string sbCost { get; set; }
        /// <summary>
        /// 大病保险上传标识
        /// </summary>
        public string sbState { get; set; }
        /// <summary>
        /// 付款对象
        /// </summary>
        public string bus_type { get; set; }
        /// <summary>
        /// 锁定
        /// </summary>
        public string lockedStatus { get; set; }
        /// <summary>
        /// 主要治疗方式
        /// </summary>
        public string cureType { get; set; }
        /// <summary>
        /// 主要治疗方式
        /// </summary>
        public string cureTypeName { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public string degree { get; set; }
        /// <summary>
        /// 就诊次数
        /// </summary>
        public string visits { get; set; }
        /// <summary>
        /// 精准民政属性
        /// </summary>
        public string jzmzName { get; set; }
        /// <summary>
        /// 精准计生属性
        /// </summary>
        public string jzjsName { get; set; }
        /// <summary>
        /// 精准目录进口目录费用
        /// </summary>
        public string jzImportCost { get; set; }
        /// <summary>
        /// 精准目录国产目录费用
        /// </summary>
        public string jzHomeCost { get; set; }
        /// <summary>
        /// 精准目录补偿金额
        /// </summary>
        public string jzCompensateCost { get; set; }
        /// <summary>
        /// 兜底补偿金额
        /// </summary>
        public string ddCost { get; set; }
        /// <summary>
        /// 交通补贴补偿
        /// </summary>
        public string trafficCost { get; set; }
        /// <summary>
        /// 生活补贴补偿
        /// </summary>
        public string liveCost { get; set; }
        /// <summary>
        /// 医疗补贴补偿
        /// </summary>
        public string medicalCost { get; set; }
        /// <summary>
        /// 新精准优抚补偿
        /// </summary>
        public string salvaYFCost { get; set; }
        /// <summary>
        /// 新精准残联补偿
        /// </summary>
        public string salvaCLCost { get; set; }
        /// <summary>
        /// 新精准扶贫补偿
        /// </summary>
        public string salvaFPCost { get; set; }
        /// <summary>
        /// 新精准疾控补偿
        /// </summary>
        public string salvaJKCost { get; set; }
        /// <summary>
        /// 新精准优抚属性
        /// </summary>
        public string salvaYFName { get; set; }
        /// <summary>
        /// 新精准残联属性
        /// </summary>
        public string salvaCLName { get; set; }
        /// <summary>
        /// 新精准扶贫属性
        /// </summary>
        public string salvaFPName { get; set; }
        /// <summary>
        /// 新精准疾控属性
        /// </summary>
        public string salvaJKName { get; set; }
        /// <summary>
        /// 兜底二次补偿金额
        /// </summary>
        public string ddSecondCost { get; set; }
        /// <summary>
        /// 意外伤害补偿
        /// </summary>
        public string accidentCost { get; set; }
        /// <summary>
        /// 特困供养
        /// </summary>
        public decimal specialPovertyCost { get; set; }
        /// <summary>
        /// 医疗扶助
        /// </summary>
        public decimal medicalAssistanceCost { get; set; }
        /// <summary>
        /// 大地保险补偿金额
        /// </summary>
        public decimal continentInsuranceCost { get; set; }
        /// <summary>
        /// 护理补贴
        /// </summary>
        public decimal nurseCost { get; set; }
    }
}