namespace Newtouch.HIS.Proxy.guian.DTO.S03
{
    /// <summary>
    /// 根据传入的医疗证号获取家庭的参合成员列表 返回报文
    /// </summary>
    public class member
    {
        /// <summary>
        /// 地区编码
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public string year { get; set; }

        /// <summary>
        /// 个人编码
        /// </summary>
        public string memberId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string memberName { get; set; }

        /// <summary>
        ///  患者性别 1男 2女
        /// </summary>
        public string memberSex { get; set; }

        /// <summary>
        /// 患者身份证号
        /// </summary>
        public string idCard { get; set; }

        /// <summary>
        /// 家庭编号
        /// </summary>
        public string familyId { get; set; }

        /// <summary>
        /// 医疗证卡号
        /// </summary>
        public string medicalNo { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string memberAge { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public string birthday { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string areaName { get; set; }

        /// <summary>
        /// 与户主关系
        /// </summary>
        public string relation { get; set; }

        /// <summary>
        /// 人员属性
        /// </summary>
        public string memberPro { get; set; }

        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal account { get; set; }

        /// <summary>
        /// 参保状态，是否停保(0 正常，1停保)
        /// </summary>
        public string memberStauts { get; set; }

        /// <summary>
        /// 普通住院总费用
        /// </summary>
        public decimal hosTotalCost { get; set; }

        /// <summary>
        /// 普通住院保内费用
        /// </summary>
        public decimal hosInsuranceCost { get; set; }

        /// <summary>
        /// 普通住院补偿费用
        /// </summary>
        public decimal hosCompensateCost { get; set; }

        /// <summary>
        /// 单病种总费用
        /// </summary>
        public decimal sigTotalCost { get; set; }

        /// <summary>
        /// 单病种保内费用
        /// </summary>
        public decimal sigInsuranceCost { get; set; }

        /// <summary>
        /// 单病种补偿费用
        /// </summary>
        public decimal sigCompensateCost { get; set; }

        /// <summary>
        /// 普通门诊总费用
        /// </summary>
        public decimal outpTotalCost { get; set; }

        /// <summary>
        /// 普通门诊保内费用
        /// </summary>
        public decimal outpInsuranceCost { get; set; }

        /// <summary>
        /// 普通门诊补偿费用
        /// </summary>
        public decimal outpCompensateCost { get; set; }

        /// <summary>
        /// 慢性病总费用
        /// </summary>
        public decimal chroTotalCost { get; set; }

        /// <summary>
        /// 慢性病保内费用
        /// </summary>
        public decimal chroInsuranceCost { get; set; }

        /// <summary>
        /// 慢性病补偿费用
        /// </summary>
        public decimal chroCompensateCost { get; set; }

        /// <summary>
        /// 精准民政属性
        /// </summary>
        public string jzmzName { get; set; }

        /// <summary>
        /// 精准计生属性
        /// </summary>
        public string jzjsName { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string tel { get; set; }

        /// <summary>
        /// 个人身份属性名称
        /// </summary>
        public string ideName { get; set; }

        /// <summary>
        /// 精准扶贫对象属性
        /// </summary>
        public string PoorObject { get; set; }

        /// <summary>
        /// 门诊累计补偿次数 
        /// </summary>
        public string currYearRedeemCount { get; set; }

        /// <summary>
        /// 门诊累计补偿额 
        /// </summary>
        public string currYearReddemMoney { get; set; }

        /// <summary>
        /// 住院累计补偿次数  
        /// </summary>
        public string inYearRedeemCount { get; set; }

        /// <summary>
        /// 住院累计补偿额 
        /// </summary>
        public string inYearRedeemMoney { get; set; }

        /// <summary>
        /// 家庭账户余额
        /// </summary>
        public string familyAccountBalance { get; set; }


    }
}