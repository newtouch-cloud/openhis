using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// 处方作废请求
    /// </summary>
    public class PrescriptionCancelRequest : RequestBase
    {
        /// <summary>
        /// 处方List
        /// </summary>
        [Required]
        public IList<PrescriptionQueryDTO> cfList { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [Required]
        public DateTime OperateTime { get; set; }

    }
}
