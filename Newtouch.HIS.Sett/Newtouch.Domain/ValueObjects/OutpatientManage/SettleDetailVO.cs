
using System;

namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    public class SettleDetailVO
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

        /// <summary>
        /// 结算开始日期
        /// </summary>
        public DateTime jsKsrq { get; set; }

        /// <summary>
        /// 结算结束日期
        /// </summary>
        public DateTime jsJsrq { get; set; }

        /// <summary>
        /// 诊疗天数
        /// </summary>
        public string ZLDate
        {
            get
            {
                decimal ksDate = 0m;
                decimal jsDate = 0m;

                TimeSpan ks = jsKsrq - jsKsrq.Date;
                TimeSpan js = jsJsrq - jsJsrq.Date;
                TimeSpan durations = jsJsrq.Date - jsKsrq.Date;

                if (ks.TotalDays - Math.Truncate(ks.TotalDays) > 0.5)
                    ksDate = 1.0m;
                else
                    ksDate = 0.5m;

                if (js.TotalDays - Math.Truncate(js.TotalDays) > 0.5)
                    jsDate = 1.0m;
                else
                    jsDate = 0.5m;

                if (ksDate == jsDate && jsDate == 0.5m || ksDate < jsDate)
                {
                    return (ksDate + Convert.ToDecimal(Math.Truncate(durations.TotalDays)) - 1 + jsDate).ToString("0.0");
                }
                else
                {
                    return (ksDate + Convert.ToDecimal(Math.Truncate(durations.TotalDays)) - 2 + jsDate).ToString("0.0");
                }
            }
        }

    }
}
