using System;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    public class PrintSettleDetailVO
    {
        /// <summary>
        /// 病人姓名
        /// </summary>
        public string Patient_Name { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public string DateOfBirth { get; set; }

        /// <summary>
        /// 清单打印编号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string MedicalRecordNo { get; set; }

        ///// <summary>
        ///// 诊疗天数
        ///// </summary>
        //public string ZLDate { get; set; }

        /// <summary>
        /// 打印 -年
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// 打印-月
        /// </summary>
        public string Month { get; set; }

        /// <summary>
        /// 打印-日
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 费用总额
        /// </summary>
        public string SummOfCharges { get; set; }

        ///// <summary>
        ///// 结算开始日期
        ///// </summary>
        //public string jsKsrq { get; set; }

        ///// <summary>
        ///// 结算结束日期
        ///// </summary>
        //public string jsJsrq { get; set; }

        /// <summary>
        /// 诊疗天数
        /// </summary>
        public string ZLDate { get; set; }

    }
}
