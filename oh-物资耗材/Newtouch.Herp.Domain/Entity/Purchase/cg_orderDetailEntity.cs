using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// 采购_订单明细
    /// </summary>
    [Table("cg_orderDetail")]
    public class CgOrderDetailEntity : IEntity<CgOrderDetailEntity>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 采购单Id
        /// </summary>
        public long orderId { get; set; }

        /// <summary>
        /// 供应商ID
        /// </summary>
        public string supplierId { get; set; }

        /// <summary>
        /// 子订单号
        /// </summary>
        public string subOrderNo { get; set; }

        /// <summary>
        /// 采购计划明细ID，cg_purchaseOrderDetail表主键
        /// </summary>
        public long pdId { get; set; }

        /// <summary>
        /// 物资Id
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// 数量，sl*zhyz=最小单位数量
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 转化英子，与单位对应
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 进价，与单位对应
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal jj { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string dwmc { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        public string unitId { get; set; }

        /// <summary>
        /// 采购订单处理流程 -1：拒处理； 0：待处理； 1：备货； 2：配送； 3：签收； 4：完成； 5：拒签 
        /// </summary>
        public int orderProcess { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 状态 0:作废；1.有效
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