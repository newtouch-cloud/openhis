namespace Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage
{
    /// <summary>
    /// 处方药品信息
    /// </summary>
    public class YP_XT_OP_PYListDTO
    {
        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 处方明细ID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string yp { get; set; }

        /// <summary>
        /// 数量 结算表数量
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string tybz { get; set; }

        /// <summary>
        /// 拆零数
        /// </summary>
        public int cls { get; set; }
    }
}
