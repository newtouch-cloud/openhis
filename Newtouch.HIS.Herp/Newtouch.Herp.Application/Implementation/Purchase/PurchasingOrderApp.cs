using System.Collections.Generic;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// 采购订单
    /// </summary>
    public class PurchasingOrderApp : AppBase, IPurchasingOrderApp
    {
        private readonly ICgOrderRepo _cgOrderRepo;

        /// <summary>
        /// 提交采购订购单
        /// </summary>
        /// <param name="cgmx"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public string SubmitGeneratingPurchaseOrder(List<VCgPurchaseOrderDetailEntity> cgmx, string userCode, string organizeId, string remark)
        {
            var dto = new SubmitGeneratingPurchaseDto
            {
                cgmx = cgmx,
                organizeId = organizeId,
                userCode = userCode,
                remark = remark
            };
            var result = new SubmitGeneratingPurchaseprocess(dto).Process();
            return result.IsSucceed ? "" : result.ResultMsg;
        }

        /// <summary>
        /// 审核采购单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="orderType"></param>
        /// <param name="remarks"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string SubmitAuditPurchasingOrder(string orderNo, int orderType, string remarks, string userCode,
            string organizeId)
        {
            var cgOrder = _cgOrderRepo.SelectData(orderNo, organizeId);
            if (cgOrder == null || cgOrder.Id <= 0) return "未找到有效的采购单";
            if (cgOrder.orderType != (int)EnumOrderTypeHrp.TempOrder || cgOrder.orderProcess != (int)EnumOrderProcess.Waiting) return "审核操作只针对待审核的暂存单";
            switch (orderType)
            {
                case (int)EnumOrderTypeHrp.TempOrder:
                case (int)EnumOrderTypeHrp.OfficialOrder:
                case (int)EnumOrderTypeHrp.BadOrder:
                    return _cgOrderRepo.AuditPurchasingOrder(orderNo, orderType, userCode, organizeId, remarks) > 0
                        ? ""
                        : "操作失败";
                default:
                    return "操作无效";
            }
        }
    }
}