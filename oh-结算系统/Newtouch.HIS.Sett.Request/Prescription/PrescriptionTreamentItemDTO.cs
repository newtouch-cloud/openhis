namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// 治疗项目明细
    /// </summary>
    public class PrescriptionTreamentItemDTO
    {
        /// <summary>
        /// 收费项目（编码）
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// 数量（计费数量）
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 单次治疗量
        /// </summary>
        public int? dczll { get; set; }

        /// <summary>
        /// 执行次数
        /// </summary>
        public int? zxcs { get; set; }

        /// <summary>
        /// 治疗量（总）
        /// </summary>
        public int? zll { get; set; }

        /// <summary>
        /// 单位（名称）
        /// </summary>
        public string dw { get; set; }
        /// <summary>
        /// 转自费标志
        /// </summary>
        public int? zzfbz { get; set; }

        public string ztId { get; set; }
        public string ztmc { get; set; }
        public int? ztsl { get; set; }
    }
}
