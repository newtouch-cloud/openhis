namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// 处方药品明细
    /// </summary>
    public class PrescriptionMedicineItemDTO
    {
        /// <summary>
        /// 药品（编码）
        /// </summary>
        public string yp { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 单位（名称）
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 剂量
        /// </summary>
        public decimal jl { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// 组号
        /// </summary>
        public string zh { get; set; }

        /// <summary>
        /// 用法代码
        /// </summary>
        public string yfCode { get; set; }
        /// <summary>
        /// 转自费标志 1:是 0：否
        /// </summary>

        public int? zzfbz { get; set; }
        public string pcCode { get; set; }
    }
}
