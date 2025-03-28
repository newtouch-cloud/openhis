namespace Newtouch.Domain.ValueObjects
{
    public class MedicineItemVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string yp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal sl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? jl { get; set; }
        /// <summary>
        /// 
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
        public int? zzfbz { get; set; }
        public string pcCode { get; set; }
    }
}
