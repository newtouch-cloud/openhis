
namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 系统药品出库明细
    /// </summary>
    public class XT_YP_LS_NBFYMXK
    {
        /// <summary>
        /// 药品代码
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// 频号
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public string Yxq { get; set; }

        /// <summary>
        /// 药库零售价
        /// </summary>
        public decimal Lsj { get; set; }

        /// <summary>
        /// 药库批发金额
        /// </summary>
        public decimal Pfj { get; set; }

        /// <summary>
        /// 零售总额
        /// </summary>
        public decimal lsje { get; set; }

        /// <summary>
        /// 发药数量 最小单位数量
        /// </summary>
        public int fysl { get; set; }

        /// <summary>
        /// 转换因子
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 申领但明细ID
        /// </summary>
        public string sldmxId { get; set; }

        /// <summary>
        /// 包装单位
        /// </summary>
        public string bzdw { get; set; }
    }
}
