using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// 采购计划
    /// </summary>
    public interface ICgPurchaseOrderDmnService
    {
        /// <summary>
        /// 获取采购计划主表信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="cgdh"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="auditState"></param>
        /// <param name="userCode">操作员账号</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VCgPurchaseOrderEntity> SelectPurchaseOrder(Pagination pagination, string cgdh, DateTime kssj, DateTime jssj, int auditState,
           string userCode, string organizeId);

        /// <summary>
        /// 查询已审核通过的采购计划单
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="cgdh"></param>
        /// <param name="deptCode"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VCgPurchaseOrderEntity> SelectAdoptPurchasingPlanInfo(Pagination pagination, string cgdh,
           string deptCode, DateTime kssj, DateTime jssj, string organizeId);

        /// <summary>
        /// 获取采购计划主表信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="cgdh"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="auditState"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VCgPurchaseOrderEntity> SelectPurchaseOrder(Pagination pagination, string cgdh, DateTime kssj, DateTime jssj, int auditState, string organizeId);

        /// <summary>
        /// 获取采购计划明细信息
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VCgPurchaseOrderDetailEntity> SelectPurchaseOrderDetail(string cgdh, string organizeId);

        /// <summary>
        /// 查询已审核通过的采购计划单
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="cgdh"></param>
        /// <param name="deptCode"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VCgPurchaseOrderEntity> SelectAdoptWaitingPurchasePlanInfo(Pagination pagination, string cgdh, string deptCode, DateTime kssj, DateTime jssj, string organizeId);

        /// <summary>
        /// 获取等待采购的采购计划明细
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VCgPurchaseOrderDetailEntity> SelectWaitingPurchasePlanDetail(string cgdh, string organizeId);
    }
}