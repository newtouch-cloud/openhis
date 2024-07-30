using System;
using System.Collections.Generic;

namespace Newtouch.Herp.Domain.DTO.InputDto
{
    /// <summary>
    /// 调价盈亏查询
    /// </summary>
    public class PriceAdjustmentProfitLossDTO
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string keyWord { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime startTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime endTime { get; set; }

        /// <summary>
        /// 查询库房
        /// </summary>
        public string kfbm { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// 是否显示零库存 1：是 0：否
        /// </summary>
        public string lkc { get; set; }

        /// <summary>
        /// 可选库房
        /// </summary>
        public List<string> kfList { get; set; }
    }
}
