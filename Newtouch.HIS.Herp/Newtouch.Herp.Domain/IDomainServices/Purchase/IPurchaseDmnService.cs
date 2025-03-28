using Newtouch.Common.Operator;
using Newtouch.Herp.Domain.Entity.Purchase;
using Newtouch.Herp.Domain.ValueObjects.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.IDomainServices.Purchase
{
    public interface IPurchaseDmnService
    {
        string PurchaseSubmit(PurchaseEntity cgEntity, List<PurchaseDetailEntity> cgmxList, string orgId, OperatorModel user);
        List<PurchaseEntity> QueryPurchasebyId(string cgId, string orgId);
        List<PurchaseDetailVO> QueryPurchaseDetailbyId(string cgId, string orgId);
        //List<PurchaseStoreDTO> QueryPurchaseStorebyId(string cgId, string orgId, string yfbmCode);

        List<ReturnedEntity> QueryPurchaseReturnbyId(string thId, string orgId);
        List<PurchaseReturnDetailVO> QueryPurchaseReturnDetailbyId(string thId, string orgId);
        List<PurchaseStoreVO> QueryPurchaseStorebyId(string cgId, string orgId, string warehouseId);

    }
}
