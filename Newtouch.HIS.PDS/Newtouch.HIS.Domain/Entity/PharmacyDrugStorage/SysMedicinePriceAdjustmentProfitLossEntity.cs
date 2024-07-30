using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统药品调价损益
    /// </summary>
    [Table("xt_yp_tjsy")]
    public class SysMedicinePriceAdjustmentProfitLossEntity : IEntity<SysMedicinePriceAdjustmentProfitLossEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string TjsyId { get; set; }

        /// <summary>
        /// 组织ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 药房部门
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 药品code
        /// </summary>
        public string ypCode{ get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// 调价时间
        /// </summary>
        public DateTime Tjsj { get; set; }

        /// <summary>
        /// 调价文件
        /// </summary>
        public string Tjwj { get; set; }

        /// <summary>
        /// 当时库存数量
        /// </summary>
        public int Dssl { get; set; }

        /// <summary>
        /// 原批发价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Ypfj { get; set; }

        /// <summary>
        /// 原零售价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Ylsj { get; set; }

        /// <summary>
        /// 原药库批发价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Yykpfj { get; set; }

        /// <summary>
        /// 原药库零售价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Yyklsj { get; set; }

        /// <summary>
        /// 现批发价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Xpfj { get; set; }

        /// <summary>
        /// 现零售价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Xlsj { get; set; }

        /// <summary>
        /// 现药库批发价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Xykpfj { get; set; }

        /// <summary>
        /// 现药库零售价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Xyklsj { get; set; }

        /// <summary>
        /// 转换因子
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// 批发价调价利润
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Pfjtjlr { get; set; }

        /// <summary>
        /// 零售价调价利润
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Lsjtjlr { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

    }
}
