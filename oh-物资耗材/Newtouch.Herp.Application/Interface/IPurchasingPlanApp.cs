using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// 采购计划
    /// </summary>
    public interface IPurchasingPlanApp
    {
        /// <summary>
        /// 提交采购计划
        /// </summary>
        /// <param name="pp"></param>
        /// <param name="ppmx"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string SubmitFillPurchasingPlan(CgPurchaseOrderEntity pp, List<CgPurchaseOrderDetailEntity> ppmx, string userCode, string organizeId);

        /// <summary>
        /// 检查是否是暂存单
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        string CheckIsTemporaryBill(string cgdh, string organizeId, ref CgPurchaseOrderEntity entity);

        /// <summary>
        /// 检查是否是审核不通过
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        string CheckIsAuditRefuseBill(string cgdh, string organizeId, ref CgPurchaseOrderEntity entity);

        /// <summary>
        /// 检查是否是审核不通过或暂存采购计划单
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        string CheckIsAuditRefuseOrTemporaryBill(string cgdh, string organizeId, ref CgPurchaseOrderEntity entity);

        /// <summary>
        /// 检查是否是暂存单
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string CancelPurchasingPlan(string cgdh, string organizeId, string userCode);

        /// <summary>
        /// 将暂存采购计划提交审核
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string SubmitPurchasingPlan(string cgdh, string organizeId);

        /// <summary>
        /// 审核采购计划
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="auditState"></param>
        /// <param name="remarks">批语</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string SubmitAuditPurchasingPlan(string cgdh, int auditState, string remarks, string organizeId, string userCode);
    }
}