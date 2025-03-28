using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// 物资_调价损益
    /// </summary>
    [Table("wz_priceAdjustmentProfitAndLoss")]
    public class WzPriceAdjustmentProfitAndLossEntity : IEntity<WzPriceAdjustmentProfitAndLossEntity>
    {
        /// <summary>
        /// 主键 调价损益序号
        /// </summary>
        [Key]
        public string TjsyId { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 库房ID
        /// </summary>
        public string warehouseId { get; set; }

        /// <summary>
        /// 物资ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 修改类型
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
        /// 原零售价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal ylsj { get; set; }

        /// <summary>
        /// 新零售价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal xlsj { get; set; }

        /// <summary>
        /// 转化因子
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 零售价调价利润
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal lsjtjlr { get; set; }
        
        /// <summary>
        /// 状态  0:作废；1.有效
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
