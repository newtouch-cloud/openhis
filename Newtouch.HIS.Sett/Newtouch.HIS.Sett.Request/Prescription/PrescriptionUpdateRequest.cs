using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// 处方修改请求
    /// </summary>
    public class PrescriptionUpdateRequest : RequestBase
    {
        /// <summary>
        /// 处方号
        /// </summary>
        [Required]
        public string cfh { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [Required]
        public DateTime OperateTime { get; set; }

        /// <summary>
        /// 删除 药品明细
        /// </summary>
        public IList<PrescriptionMedicineItemDTO> DeleteMedicineItems { get; set; }

        /// <summary>
        /// 删除 治疗项目明细
        /// </summary>
        public IList<PrescriptionTreamentItemDTO> DeleteTreamentItems { get; set; }

        /// <summary>
        /// 新增 药品明细
        /// </summary>
        public IList<PrescriptionMedicineItemDTO> AddMedicineItems { get; set; }

        /// <summary>
        /// 新增 治疗项目明细
        /// </summary>
        public IList<PrescriptionTreamentItemDTO> AddTreamentItems { get; set; }

    }
}
