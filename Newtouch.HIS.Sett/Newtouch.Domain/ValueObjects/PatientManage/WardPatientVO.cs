using System;

namespace Newtouch.HIS.Domain.ValueObjects.PatientManage
{
    /// <summary>
    /// 记账待执行 病人 VO
    /// </summary>
    public class PendingExecutionPatientVO
    {
        /// <summary>
        /// 病区Code
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// 病人id
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 病人姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 医保剩余次数
        /// </summary>
        public int? ybsycs { get; set; }
        /// <summary>
        /// 执行状态
        /// </summary>
        public int? zxzt { get; set; }
        public string blh { get; set; }
        public string py { get; set; }
        public DateTime? ghrq { get; set; }
        public DateTime? CreateTime { get; set; }

    }
}
