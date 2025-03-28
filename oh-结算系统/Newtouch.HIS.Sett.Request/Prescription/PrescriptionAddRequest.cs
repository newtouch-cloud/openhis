using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// 处方新增请求
    /// </summary>
    public class PrescriptionAddRequest : RequestBase
    {
        /// <summary>
        /// 病历号
        /// </summary>
        [Required]
        public string blh { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>
        [Required]
        public string mzh { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        [Required]
        public string cfh { get; set; }

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
        /// 药品明细
        /// </summary>
        [Required]
        public IList<PrescriptionMedicineItemDTO> MedicineItems { get; set; }

        /// <summary>
        /// 治疗项目明细
        /// </summary>
        [Required]
        public IList<PrescriptionTreamentItemDTO> TreamentItems { get; set; }

    }
}
