using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// 处方新增/明细删除请求
    /// </summary>
    public class PrescriptionAddOrUpdateRequest : RequestBase
    {
        /// <summary>
        /// 门诊号
        /// </summary>
        [Required]
        public string mzh { get; set; }

        /// <summary>
        /// 医生（医生工号）
        /// </summary>
        [Required]
        public string ys { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [Required]
        public DateTime OperateTime { get; set; }

        /// <summary>
        /// 处方 List
        /// </summary>
        [Required]
        public IList<PrescriptionUpdateDTO> PrescriptionList { get; set; }

    }
}
