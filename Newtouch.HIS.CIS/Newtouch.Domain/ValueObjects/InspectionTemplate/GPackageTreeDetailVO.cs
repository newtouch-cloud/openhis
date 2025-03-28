namespace Newtouch.Domain.ValueObjects
{
    public class GPackageTreeDetailVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string ztId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ztmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xmCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xmdm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xmmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? dj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zxks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zxksmc { get; set; }
        public string sfyb { get; set; }
        public string sqlx { get; set; }


    }

    public class GPackageZTTreeDetailVO : GPackageTreeDetailVO
    {
        public string bw { get; set; }
        public int sl { get; set; }
		public int? px { get; set; }
        /// <summary>
        /// 组合金额
        /// </summary>
        public decimal? zhje { get; set; }
    }

}
