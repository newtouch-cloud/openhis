using System;
using Newtouch.HIS.Domain.DTO;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 门诊退费 结算 主记录 Query
    /// </summary>
    public class OutPatientRefundableJsVO : OutpatientSettYbFeeRelatedDTO
    {
        /// <summary>
        /// 结算内码
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// 收费日期
        /// </summary>
        public DateTime? sfrq { get; set; }

        /// <summary>
        /// 结算人员
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 结算人员姓名
        /// </summary>
        public string CreatorUserName { get; set; }

        /// <summary>
        /// 门诊挂号 或 门诊收费记账
        /// </summary>
        public string jslx { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 医保结算 医保结算号
        /// </summary>
        public string ybjsh { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal jszje { get; set; }

        /// <summary>
        /// 结算支付
        /// </summary>
        public decimal jsxjzf { get; set; }

        /// <summary>
        /// 现金支付方式名称
        /// </summary>
        public string xjzffsmc { get; set; }

    }

    /// <summary>
    /// 退费信息
    /// </summary>
    public class OutPatientRefundableGuiAnJsVO
    {
        /// <summary>
        /// 结算内码
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// 收费日期
        /// </summary>
        public DateTime? sfrq { get; set; }

        /// <summary>
        /// 结算人员
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 结算人员姓名
        /// </summary>
        public string CreatorUserName { get; set; }

        /// <summary>
        /// 门诊挂号 或 门诊收费记账
        /// </summary>
        public string jslx { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 医保结算 医保结算号
        /// </summary>
        public string ybjsh { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal jszje { get; set; }

        /// <summary>
        /// 结算支付
        /// </summary>
        public decimal jsxjzf { get; set; }

        /// <summary>
        /// 现金支付方式名称
        /// </summary>
        public string xjzffsmc { get; set; }
        /// <summary>
        /// 就诊编号
        /// </summary>
        public string prm_akc190 { get; set; }
        /// <summary>
        /// 分中心编号
        /// </summary>
        public string prm_yab003 { get; set; }
        /// <summary>
        /// 支付类别
        /// </summary>
        public string prm_aka130 { get; set; }
        /// <summary>
        /// 结算编号
        /// </summary>
        public string prm_yka103 { get; set; }
        /// <summary>
        /// 经办人员编码
        /// </summary>
        public string prm_aae011 { get; set; }
        /// <summary>
        /// 经人人姓名
        /// </summary>
        public string prm_ykc141 { get; set; }
        /// <summary>
        /// 经办时间
        /// </summary>
        public string prm_aae036 { get; set; }
        /// <summary>
        /// 退费原因
        /// </summary>
        public string prm_aae013 { get; set; }
        /// <summary>
        /// 社会保险办法
        /// </summary>
        public string prm_ykb065 { get; set; }
        /// <summary>
        /// 个人编号
        /// </summary>
        public string prm_aac001 { get; set; }

        /// <summary>
        /// 门诊补偿序号
        /// </summary>
        public string outpId { get; set; }
    }
}
