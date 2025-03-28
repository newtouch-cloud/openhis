using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// 调价信息
    /// </summary>
    public class VPriceAdjustmentInfoEntity
    {
        /// <summary>
        /// 主键 物资调价Id
        /// </summary>
        public string wztjId { get; set; }

        /// <summary>
        /// 审核状态   0:未审核 1:已审核 2:已拒绝 3.已撤销
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// 执行标志   0:未执行 1:已执行
        /// </summary>
        public string zxbz { get; set; }

        /// <summary>
        /// 列别名称
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
        /// 原零售价 与转化因子对应，一般为部门单位价格
        /// </summary>
        public decimal ylsj { get; set; }

        /// <summary>
        /// 零售价 与转化因子对应，一般为部门单位价格
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// 单位 与zhyz对应，一般为部门单位
        /// </summary>
        public string dwmc { get; set; }

        /// <summary>
        /// 转化因子
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 生产厂商
        /// </summary>
        public string supplierName { get; set; }

        /// <summary>
        /// 状态  0:作废；1.有效
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 调整文件
        /// </summary>
        public string tzwj { get; set; }

        /// <summary>
        /// 调整时间
        /// </summary>
        public DateTime tzsj { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime? zxsj { get; set; }

        /// <summary>
        /// 是否过期
        /// </summary>
        public string Isgq { get; set; }

        /// <summary>
        /// 物资ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// 提交人员
        /// </summary>
        public string CreatorCode { get; set; }
    }
}
