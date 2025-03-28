namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 报损报益总金额查询
    /// </summary>
    public class LossAndProditInfoJeVo
    {
        /// <summary>
        /// 零售价总金额
        /// </summary>
        public decimal? Ljze { get; set; }

        /// <summary>
        /// 批发价总金额
        /// </summary>
        public decimal? Pjze { get; set; }

        /// <summary>
        /// 进价总金额
        /// </summary>
        public decimal? Jjze { get; set; }
    }
}
