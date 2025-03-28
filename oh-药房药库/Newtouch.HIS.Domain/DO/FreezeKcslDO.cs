using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DO
{
    /// <summary>
    /// 冻结库存信息
    /// </summary>
    public class FreezeKcslDO
    {

        /// <summary>
        /// 被冻结的药房/库编码
        /// </summary>
        public string YfbmCode { get; set; }

        /// <summary>
        /// 被冻结药品信息
        /// </summary>
        public List<FreezeKcslDetailDO> Detail { get; set; }

        /// <summary>
        /// 操作人编码
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 组织机构编码
        /// </summary>
        public string OrganizeId { get; set; }
    }

    /// <summary>
    /// 被冻结药品信息
    /// </summary>
    public class FreezeKcslDetailDO
    {

        /// <summary>
        /// 药品代码
        /// </summary>
        public string YpCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string Ypmc { get; set; }

        /// <summary>
        /// 需要冻结药品总数量（最小单位）
        /// </summary>
        public int Kcsl { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string Pc { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? Yxq { get; set; }
    }


    /// <summary>
    /// 冻结库存信息
    /// </summary>
    public class UnFreezeKcslDO
    {

        /// <summary>
        /// 被冻结的药房/库编码
        /// </summary>
        public string YfbmCode { get; set; }

        /// <summary>
        /// 取消冻结药品信息
        /// </summary>
        public List<UnFreezeKcslDetailDO> Detail { get; set; }

        /// <summary>
        /// 操作人编码
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 组织机构编码
        /// </summary>
        public string OrganizeId { get; set; }
    }

    /// <summary>
    /// 取消冻结药品信息
    /// </summary>
    public class UnFreezeKcslDetailDO
    {

        /// <summary>
        /// 药品代码
        /// </summary>
        public string YpCode { get; set; }

        /// <summary>
        /// 需要取消冻结药品总数量（最小单位）
        /// </summary>
        public int Kcsl { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string Pc { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string Ph { get; set; }
    }

    public class ReturningInventoryDO
    {
        /// <summary>
        /// 需要加库存的药房部门编码
        /// </summary>
        public string NeedAddInventoryYfbmCode { get; set; }

        /// <summary>
        /// 需要减库存的药房部门编码
        /// </summary>
        public string NeedSubtractInventoryYfbmCode { get; set; }

        /// <summary>
        /// 需要还的库存明细
        /// </summary>
        public List<InventoryDetailDO> Detail { get; set; }

        /// <summary>
        /// 操作人编码
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 组织机构编码
        /// </summary>
        public string OrganizeId { get; set; }
    }

    /// <summary>
    /// 库存明细
    /// </summary>
    public class InventoryDetailDO
    {

        /// <summary>
        /// 药品代码
        /// </summary>
        public string YpCode { get; set; }

        /// <summary>
        /// 需要取消冻结药品总数量（最小单位）
        /// </summary>
        public int Kcsl { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string Pc { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string Ph { get; set; }

    }
}
