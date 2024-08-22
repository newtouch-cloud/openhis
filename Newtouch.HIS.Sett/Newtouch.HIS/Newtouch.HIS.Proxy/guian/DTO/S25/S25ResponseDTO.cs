using System.Collections.Generic;

namespace Newtouch.HIS.Proxy.guian.DTO.S25
{
    /// <summary>
    /// 根据S18门诊上传返回的补偿序号outpId进行门诊结算 返回报文
    /// </summary>
    public class S25ResponseDTO
    {

        /// <summary>
        /// 总费用（四位小数精度）
        /// </summary>
        public decimal totalCost { get; set; }

        /// <summary>
        /// 保内费用（四位小数精度）
        /// </summary>
        public decimal insuranceCost { get; set; }

        /// <summary>
        /// 起伏线（四位小数精度）
        /// </summary>
        public decimal undulatingLine { get; set; }

        /// <summary>
        /// 统筹补偿（四位小数精度）
        /// </summary>
        public string generalRedeem { get; set; }

        /// <summary>
        /// 家庭账户补偿（四位小数精度）
        /// </summary>
        public decimal accRedeem { get; set; }

        /// <summary>
        /// 农合补偿费用（四位小数精度）
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
        /// 精准民政救助补偿（四位小数精度）
        /// </summary>
        public string salvaMZCost { get; set; }

        /// <summary>
        /// 兜底补偿费用（四位小数精度）
        /// </summary>
        public string bottomRedeem { get; set; }

        /// <summary>
        /// 兜底二次补偿费用
        /// </summary>
        public string bottomSecondRedeem { get; set; }

        /// <summary>
        /// 精准目录补偿（四位小数精度）
        /// </summary>
        public string medicineCost { get; set; }

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
        /// 计算进度
        /// </summary>
        public List<calcProgress> list { get; set; }

        /// <summary>
        /// 大地保险补偿金额
        /// </summary>
        public string continentInsuranceCost { get; set; }

        /// <summary>
        /// 特困供养补偿
        /// </summary>
        public string specialPovertyCost { get; set; }

        /// <summary>
        /// 医疗扶助补偿
        /// </summary>
        public string medicalAssistanceCost { get; set; }

        /// <summary>
        /// 合医二次补偿
        /// </summary>
        public string medicalSecondCost { get; set; }

        /// <summary>
        /// 护理补偿
        /// </summary>
        public decimal nurseCost { get; set; }

    }
}