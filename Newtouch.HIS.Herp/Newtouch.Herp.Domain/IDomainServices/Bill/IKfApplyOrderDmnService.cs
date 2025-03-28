using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// 申领单管理
    /// </summary>
    public interface IKfApplyOrderDmnService
    {
        /// <summary>
        /// 获取申领单主信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldh"></param>
        /// <param name="applyType"></param>
        /// <param name="applyProcess"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VApplyBillInfoEntity> SelectApplyBillInfo(Pagination pagination, string sldh, int applyType,
           int applyProcess, DateTime kssj, DateTime jssj, string organizeId);

        /// <summary>
        /// 获取申领单主信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldh"></param>
        /// <param name="applyType"></param>
        /// <param name="applyProcess"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="organizeId"></param>
        /// <param name="warehouseId"></param>
        /// <param name="applyProcesses">可选处理状态集合</param>
        /// <returns></returns>
        IList<VApplyBillInfoEntity> SelectApplyBillInfo(Pagination pagination, string sldh, int applyType,
            int applyProcess, DateTime kssj, DateTime jssj, string organizeId, string warehouseId, List<int> applyProcesses);

        /// <summary>
        /// 获取申领单主明细
        /// </summary>
        /// <param name="sldh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VApplyBillDetailEntity> SelectApplyBillDetail(string sldh, string organizeId);

        /// <summary>
        /// 获取申领单主明细
        /// </summary>
        /// <param name="sldhs"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VApplyBillDetailEntity> SelectApplyBillDetailBySldhs(string sldhs, string organizeId);
    }
}