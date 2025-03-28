namespace HIS.SSO.Domain.ValueObjects.SysManage
{
    public class HomeDataTotalVo : ystjVo
    {
        public string? brxz { get; set; }
        /// <summary>
        /// 挂号数
        /// </summary>
        public int? ghrc { get; set; }
        /// <summary>
        /// 退号数
        /// </summary>
        public int? thrc { get; set; }
        /// <summary>
        /// 结算金额
        /// </summary>
        public decimal? jsje { get; set; }
        /// <summary>
        /// 退费金额
        /// </summary>
        public decimal? tfje { get; set; }

    }
    public class ystjVo : hstjVo
    {
        public int? cflx { get; set; }
        /// <summary>
        /// 就诊人数
        /// </summary>
        public int? jzrc { get; set; }
        /// <summary>
        /// 开方金额
        /// </summary>
        public decimal? kfje { get; set; }
        /// <summary>
        /// 结算金额
        /// </summary>
        public decimal? jsje { get; set; }
    }
    public class hstjVo
    {
        /// <summary>
        /// 已入区人数
        /// </summary>
        public int? yrqrc { get; set; }
        /// <summary>
        /// 未入区人数
        /// </summary>
        public int? wrqrc { get; set; }
        /// <summary>
        /// 今日未出区数
        /// </summary>
        public int? jrwcqrc { get; set; }
        /// <summary>
        /// 今日出区数
        /// </summary>
        public int? jrcqrc { get; set; }
        /// <summary>
        /// 总床位
        /// </summary>
        public int? zcw { get; set; }
        /// <summary>
        /// 剩余床位
        /// </summary>
        public int? sycw { get; set; }
    }
}
