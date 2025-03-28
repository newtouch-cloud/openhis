using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// 采购单信息
    /// </summary>
    public class VCgOrderEntity
    {

        /// <summary>
        /// 订单类型 0：暂存单；1：正式单
        /// </summary>
        public int orderType { get; set; }

        /// <summary>
        /// 采购订单处理流程 -1：拒处理； 0：待处理； 1：备货； 2：配送； 3：签收； 4：完成； 5：拒签 
        /// </summary>
        public int orderProcess { get; set; }

        /// <summary>
        /// 采购单号
        /// </summary>
        public string orderNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string LastModifierName { get; set; }
    }

    /// <summary>
    /// 采购单明细
    /// </summary>
    public class VCgOrderDetailEntity
    {
        /// <summary>
        /// 子订单号
        /// </summary>
        public string subOrderNo { get; set; }

        /// <summary>
        /// 采购计划单号
        /// </summary>
        public string cgdh { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string deptName { get; set; }

        /// <summary>
        /// 物资名称
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// 数量+单位
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string brand { get; set; }

        /// <summary>
        ///进价+单位
        /// </summary>
        public string jjStr { get; set; }

        /// <summary>
        /// 厂家名称
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string gysmc { get; set; }

        /// <summary>
        /// 明细备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public string productId { get; set; }
    }
}