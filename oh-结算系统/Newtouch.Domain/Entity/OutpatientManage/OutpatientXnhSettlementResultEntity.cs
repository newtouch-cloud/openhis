using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 新农合门诊结算结果
    /// </summary>
    [Table("mz_xnh_settResult")]
    public class OutpatientXnhSettlementResultEntity : IEntity<OutpatientXnhSettlementResultEntity>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 结算内码
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// 补偿序号
        /// </summary>
        public string outpId { get; set; }

        /// <summary>
        /// 总费用（四位小数精度）
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal totalCost { get; set; }

        /// <summary>
        /// 总费用（四位小数精度）
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal insuranceCost { get; set; }

        /// <summary>
        /// 起伏线（四位小数精度）
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal undulatingLine { get; set; }

        /// <summary>
        /// 统筹补偿（四位小数精度）
        /// </summary>
        public string generalRedeem { get; set; }

        /// <summary>
        /// 家庭账户补偿（四位小数精度）
        /// </summary>
        public string accRedeem { get; set; }

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
        /// 精准民政救助补偿（四位小数精度）
        /// </summary>
        public string salvaMZCost { get; set; }

        /// <summary>
        /// 结算状态 1-已结 2-已退   默认已结
        /// </summary>
        public int jszt { get; set; }

        /// <summary>
        /// 状态 1-有效  0-无效
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改者
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}