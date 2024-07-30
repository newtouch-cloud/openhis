
namespace Newtouch.HIS.Domain.ValueObjects
{
    public class OrderMzVO
    {
        public string OrderNo { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }
        /// <summary>
        /// 处方金额
        /// </summary>
        public decimal? cfje { get; set; }
        /// <summary>
        /// 处方明细
        /// </summary>
        public string cfxmmx { get; set; }
        /// <summary>
        /// 处方收费项目
        /// </summary>
        public string sfxm { get; set; }
        /// <summary>
        /// 处方收费项目名称
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// 处方类型 EnumCflx
        /// </summary>
        public string cflx { get; set; }
        /// <summary>
        /// 是否药品处方
        /// </summary>
        public string isyp { get; set; }
        /// <summary>
        /// 处方说明
        /// </summary>
        public string cfmc { get; set; }
    }
}
