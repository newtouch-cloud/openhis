using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// 处方
    /// </summary>
    public class PrescriptionQueryDTO
    {
        /// <summary>
        /// 处方类型 对应枚举 EnumPrescriptionType
        /// </summary>
        [Required]
        public int cflx { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        [Required]
        public string cfh { get; set; }

    }
}
