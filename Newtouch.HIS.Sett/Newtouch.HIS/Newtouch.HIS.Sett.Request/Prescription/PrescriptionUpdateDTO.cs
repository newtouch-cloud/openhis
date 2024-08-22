using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// 处方信息DTO
    /// </summary>
    public class PrescriptionUpdateDTO
    {
        /// <summary>
        /// 处方类型 对应枚举 EnumPrescriptionType
        /// </summary>
        [Required]
        public int cflx { get; set; }

        /// <summary>
        /// 处方类型名称
        /// </summary>
        public string cflxmc { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        [Required]
        public string cfh { get; set; }

        /// <summary>
        /// 领药药房
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// 中药贴数
        /// </summary>
        public int? ts { get; set; }

        /// <summary>
        /// 中药代煎标志 1 需要代煎
        /// </summary>
        public bool? djbz { get; set; }

        /// <summary>
        /// 删除 药品明细
        /// 删除可以以编码来判断唯一
        /// </summary>
        public IList<PrescriptionMedicineItemDTO> DeleteMedicineItems { get; set; }

        /// <summary>
        /// 删除 治疗项目明细
        /// 删除可以以编码来判断唯一
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
