using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// 提交损益信息
    /// </summary>
    public class VPriceAdjustmentProfitLossEntity
    {
        /// <summary>
        /// 调价损益ID
        /// </summary>
        public string TjsyId { get; set; }

        /// <summary>
        /// 物资ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string lbmc { get; set; }

        /// <summary>
        /// 物资名称
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string supplierName { get; set; }

        /// <summary>
        /// 库房
        /// </summary>
        public string warehouseId { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 调价时间
        /// </summary>
        public DateTime tjsj { get; set; }

        /// <summary>
        /// 调价文件
        /// </summary>
        public string tjwj { get; set; }

        /// <summary>
        /// 当时数量
        /// </summary>
        public int dssl { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string dwmc { get; set; }

        /// <summary>
        /// 原零售价
        /// </summary>
        public decimal ylsj { get; set; }

        /// <summary>
        /// 先零售价
        /// </summary>
        public decimal xlsj { get; set; }

        /// <summary>
        /// 转化因子 与ylsj、xlsj、dssl对应
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 零售价调价利润
        /// </summary>
        public decimal lsjtjlr { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatorCode { get; set; }
    }
}
