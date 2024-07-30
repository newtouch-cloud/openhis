namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 进销存统计对象
    /// </summary>
    public class PSIStatisticsVO
    {
        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 零售价
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? qcsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? qclsze { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? qcjjze { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? rksl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? rklsze { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? rkjjze { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? cksl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? cklsze { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? ckjjze { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? sysl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? sylsze { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? syjjze { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? tjze { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? qmsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? qmlsze { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public decimal? qmjjze { get; set; }
    }
}
