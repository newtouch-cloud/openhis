using System;

namespace Newtouch.Herp.Domain.DTO.InputDto
{
    /// <summary>
    /// 调价提交
    /// </summary>
    public class SubmitPriceAdjustmentRequestDTO
    {
        /// <summary>
        /// 物资ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// 转化因子
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 零售价 与转化因子对应，一般为部门单位价格
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// 原零售价 与转化因子对应，一般为部门单位价格
        /// </summary>
        public decimal ylsj { get; set; }

        /// <summary>
        /// 单位 与zhyz对应，一般为部门单位
        /// </summary>
        public string dwmc { get; set; }

        /// <summary>
        /// 调整文件
        /// </summary>
        public string tzwj { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime zxsj { get; set; }
    }
}
