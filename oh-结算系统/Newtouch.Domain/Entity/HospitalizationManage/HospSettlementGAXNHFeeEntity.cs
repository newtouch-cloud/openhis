using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 创 建：李鑫
    /// 日 期：2019-07-19 14:00
    /// 描 述：住院结算-医保费用-贵安
    /// </summary>
    [Table("zy_js_gaxnhjyfy")]
    public class HospSettlementGAXNHFeeEntity : IEntity<HospSettlementGAXNHFeeEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        public string inpId { get; set; }
        
        /// <summary>
        /// 关联zy_js jsnm
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// 总费用（四位小数精度）
        /// </summary>
        public decimal? totalCost { get; set; }
        /// <summary>
        /// 保内费用（四位小数精度）
        /// </summary>
        public decimal? insuranceCost { get; set; }
        /// <summary>
        /// 起伏线（四位小数精度）
        /// </summary>
        public decimal? undulatingLine { get; set; }
        /// <summary>
        /// 统筹补偿（四位小数精度）
        /// </summary>
        public decimal? generalRedeem { get; set; }
        /// <summary>
        /// 意外伤害补偿（四位小数精度）
        /// </summary>
        public decimal? accidentRedeem { get; set; }
        /// <summary>
        /// 家庭账户补偿（四位小数精度）
        /// </summary>
        public decimal? accRedeem { get; set; }
        /// <summary>
        /// 补偿费用（四位小数精度）
        /// </summary>
        public string compensateCost { get; set; }
        /// <summary>
        /// 民政救助费用（四位小数精度）
        /// </summary>
        public string civilCost { get; set; }
        /// <summary>
        /// 大病商保补偿（四位小数精度）
        /// </summary>
        public string insureCost { get; set; }
        /// <summary>
        /// 计生救助费用（四位小数精度）
        /// </summary>
        public string salvaJSCost { get; set; }
        /// <summary>
        /// 兜底补偿费用（四位小数精度）
        /// </summary>
        public string bottomRedeem { get; set; }
        /// <summary>
        /// 兜底二次补偿费用（四位小数精度）
        /// </summary>
        public string bottomSecondRedeem { get; set; }
        /// <summary>
        /// 精准目录补偿（四位小数精度）
        /// </summary>
        public string medicineCost { get; set; }
        /// <summary>
        /// 交通补贴补偿（四位小数精度）
        /// </summary>
        public string trafficCost { get; set; }
        /// <summary>
        /// 生活补贴补偿（四位小数精度）
        /// </summary>
        public string liveCost { get; set; }
        /// <summary>
        /// 医疗补贴补偿（四位小数精度）
        /// </summary>
        public string medicalCost { get; set; }
        /// <summary>
        /// 新精准优抚补偿（四位小数精度）
        /// </summary>
        public string salvaYFCost { get; set; }
        /// <summary>
        /// 新精准残联补偿（四位小数精度）
        /// </summary>
        public string salvaCLCost { get; set; }
        /// <summary>
        /// 新精准扶贫补偿（四位小数精度）
        /// </summary>
        public string salvaFPCost { get; set; }
        /// <summary>
        /// 新精准疾控补偿（四位小数精度）
        /// </summary>
        public string salvaJKCost { get; set; }
        /// <summary>
        /// 成员编码
        /// </summary>
        public string memberNo { get; set; }
        /// <summary>
        /// 成员姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 医疗证、卡号
        /// </summary>
        public string bookNo { get; set; }
        /// <summary>
        /// 性别名称
        /// </summary>
        public string sexName { get; set; }
        /// <summary>
        /// 出生年月日
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// 户主姓名
        /// </summary>
        public string masterName { get; set; }
        /// <summary>
        /// 与户主关系名称
        /// </summary>
        public string relationName { get; set; }
        /// <summary>
        /// 个人身份属性名称
        /// </summary>
        public string identityName { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string idCard { get; set; }
        /// <summary>
        /// 当前年度成员住院已补偿次数
        /// </summary>
        public string currYearRedeemCount { get; set; }
        /// <summary>
        /// 当前年度成员住院已补偿总医疗费用（单位元，小数点后保留两位）
        /// </summary>
        public string currYearTotal { get; set; }
        /// <summary>
        /// 当前年度成员住院已补偿总保内费用（单位元，小数点后保留两位）
        /// </summary>
        public string currYearEnableMoney { get; set; }
        /// <summary>
        /// 当前年度成员住院已补偿金额（单位元，小数点后保留两位）
        /// </summary>
        public string currYearReddemMoney { get; set; }
        /// <summary>
        /// 成员家庭编码
        /// </summary>
        public string familyNo { get; set; }
        /// <summary>
        /// 成员家庭住址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 参合属性名称
        /// </summary>
        public string joinPropName { get; set; }
        /// <summary>
        /// 当前年度家庭住院已补偿次数
        /// </summary>
        public string currFamilyRedeemCount { get; set; }
        /// <summary>
        /// 当前年度家庭住院已补偿总医疗费用（单位元，小数点后保留两位）
        /// </summary>
        public string currFamilyTotal { get; set; }
        /// <summary>
        /// 当前年度家庭住院已补偿保内费用（单位元，小数点后保留两位）
        /// </summary>
        public string currFamilyEnableMoney { get; set; }
        /// <summary>
        /// 当前年度家庭住院已补偿金额（单位元，小数点后保留两位）
        /// </summary>
        public string currFamilyReddemMoney { get; set; }
        /// <summary>
        /// 本次住院总医疗费用（单位元，小数点后保留两位）
        /// </summary>
        public string totalCosts { get; set; }
        /// <summary>
        /// 本次住院保内费用（单位元，小数点后保留两位）
        /// </summary>
        public string enableMoney { get; set; }
        /// <summary>
        /// 本次住院费用中国定基本药品费用(单位元,小数点后保留两位)
        /// </summary>
        public string essentialMedicineMoney { get; set; }
        /// <summary>
        /// 本次住院费用中省补基本药品费用(单位元,小数点后保留两位)
        /// </summary>
        public string provinceMedicineMoney { get; set; }
        /// <summary>
        /// 本次住院补偿扣除起付线金额（单位元，小数点后保留两位）
        /// </summary>
        public string startMoney { get; set; }
        /// <summary>
        /// 本次住院补偿金额（单位元，小数点后保留两位）
        /// </summary>
        public string calculateMoney { get; set; }
        /// <summary>
        /// 补偿类型名称
        /// </summary>
        public string redeemTypeName { get; set; }
        /// <summary>
        /// 是否为单病种补偿
        /// </summary>
        public string isSpecial { get; set; }
        /// <summary>
        /// 是否实行保底补偿
        /// </summary>
        public string isPaul { get; set; }
        /// <summary>
        /// 追补金额，中药和国定基本药品提高补偿额部分（单位元，小数点后保留两位）
        /// </summary>
        public string anlagernMoney { get; set; }
        /// <summary>
        /// 单病种费用定额（单位元，小数点后保留两位）
        /// </summary>
        public string fundPayMoney { get; set; }
        /// <summary>
        /// 医疗机构承担费用(单位元，小数点后保留两位)
        /// </summary>
        public string hospAssumeMoney { get; set; }
        /// <summary>
        /// 个人自付费用（单位元，小数点后保留两位）
        /// </summary>
        public string personalPayMoney { get; set; }
        /// <summary>
        /// 民政优抚医疗补助
        /// </summary>
        public string YFmedicalAid { get; set; }
        /// <summary>
        /// 民政城乡医疗救助
        /// </summary>
        public string CXmedicalAid { get; set; }
        /// <summary>
        /// 高额材料限价超额费用（单位元，小数点后保留两位）
        /// </summary>
        public string materialMoney { get; set; }
        /// <summary>
        /// 补偿分段信息列表
        /// </summary>
        public string gradeList { get; set; }
        /// <summary>
        /// 本次结算计算方法
        /// </summary>
        public string calculationMethod { get; set; }
        /// <summary>
        /// 慈善总会支付金额
        /// </summary>
        public string ChinaCharityPay { get; set; }
        /// <summary>
        /// 是否长周期定额付费
        /// </summary>
        public string isLongPeriod { get; set; }
        /// <summary>
        /// 是否进入大病保险
        /// </summary>
        public string isCII { get; set; }
        /// <summary>
        /// 大病保险合规费用
        /// </summary>
        public string CIIEligibleCosts { get; set; }
        /// <summary>
        /// 本次大病保险起付线
        /// </summary>
        public string CIIStartMoney { get; set; }
        /// <summary>
        /// 本次大病保险补偿金额
        /// </summary>
        public string CIICalculateMoney { get; set; }
        /// <summary>
        /// 累计大病保险补偿额
        /// </summary>
        public string CIICumulativePay { get; set; }
        /// <summary>
        /// 累计大病保险扣除起付线金额
        /// </summary>
        public string CIICumulativeStart { get; set; }
        /// <summary>
        /// 累计进入大病保险合规费用
        /// </summary>
        public string CIICumulativeEligible { get; set; }
        /// <summary>
        /// 计生两户减免费用金额
        /// </summary>
        public string FamilyPlanningWaiver { get; set; }
        /// <summary>
        /// 其他补偿
        /// </summary>
        public string OtherPay { get; set; }
        /// <summary>
        /// 家庭账户支付
        /// </summary>
        public string familyAccount { get; set; }
        /// <summary>
        /// 大地保险补偿金额
        /// </summary>
        public string continentInsuranceCost { get; set; }
        /// <summary>
        /// 护理补贴
        /// </summary>
        public string nurseCost { get; set; }
        /// <summary>
        /// 床位包干补偿金额
        /// </summary>
        public string cwAccountCost { get; set; }
        /// <summary>
        /// 特困供养补偿
        /// </summary>
        public string specialPovertyCost { get; set; }
        /// <summary>
        /// 医疗扶助补偿
        /// </summary>
        public string medicalAssistanceCost { get; set; }
        /// <summary>
        /// 精准民政救助补偿（四位精准小数）
        /// </summary>
        public string salvaMZCost { get; set; }
        /// <summary>
        /// 其他统筹金额
        /// </summary>
        public decimal? salvaQTCost { get; set; }
        /// <summary>
        /// 农合总金额
        /// </summary>
        public decimal? nhzje { get; set; }
        /// <summary>
        /// 农合现金支付
        /// </summary>
        public decimal? nhxjzf { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

    }
}