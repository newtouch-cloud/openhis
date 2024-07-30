using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// 出入库单据明细
    /// </summary>
    [Table("kf_crkmx")]
    public class KfCrkmxEntity : IEntity<KfCrkmxEntity>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 出入库单据ID
        /// </summary>
        public long crkId { get; set; }

        /// <summary>
        /// 申领单明细ID
        /// </summary>
        public long? applyDetailId { get; set; }

        /// <summary>
        /// 采购单Id
        /// </summary>
        public long? purchaseId { get; set; }

        /// <summary>
        /// 物资ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// 数量，sl*zhyz=最小单位数量
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        public string unitId { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string unitName { get; set; }

        /// <summary>
        /// 转化因子
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime yxq { get; set; }

        /// <summary>
        /// 进价，与sl同单位
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal jj { get; set; }

        /// <summary>
        /// 零售价，与sl同单位
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal lsj { get; set; }

        /// <summary>
        /// 入库部门库存，操作前统计，最小单位
        /// </summary>
        public int rkbmkc { get; set; }

        /// <summary>
        /// 出库部门库存，操作前统计，最小单位
        /// </summary>
        public int ckbmkc { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime? scrq { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 状态
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
