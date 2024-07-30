using System;

namespace Newtouch.Herp.Domain.DTO.InputDto
{
    /// <summary>
    /// 出入库单据查询条件
    /// </summary>
    public class CrkdjSearchParamDTO
    {
        /// <summary>
        /// 审核状态
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime kssj { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime jssj { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// 票单号
        /// </summary>
        public string pdh { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        public string djlx { get; set; }

        /// <summary>
        /// 配送单号
        /// </summary>
        public string deliveryNo { get; set; }
    }
}
