using Newtouch.HIS.API.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// 处方作废请求根据门诊号
    /// </summary>
    public class PrescriptionCancelByOutNoRequest : RequestBase
    {
        /// <summary>
        /// 处方号
        /// </summary>
        [Required]
        public string mzh { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [Required]
        public DateTime OperateTime { get; set; }

    }
}