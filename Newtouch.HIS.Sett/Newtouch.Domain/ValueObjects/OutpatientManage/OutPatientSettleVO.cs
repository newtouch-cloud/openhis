using Newtouch.HIS.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 门诊收费对象
    /// </summary>
    [NotMapped]
    public class OutPatientSettleVO: OutpatientRegistEntity
    {
        /// <summary>
        /// 病人性质名称
        /// </summary>
        public string brxzmc { get; set; }
        /// <summary>
        /// 大病项目名称
        /// </summary>
        public string dbxmmc { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal zje { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// 人员名称
        /// </summary>
        public string rymc { get; set; }
        /// <summary>
        /// 作废日期
        /// </summary>
        public DateTime? zfrq { get; set; }
        /// <summary>
        /// 收费项目名称
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// 作废
        /// </summary>
        public string iszf { get; set; }
        /// <summary>
        /// 退回
        /// </summary>
        public string isth { get; set; }


    }
}
