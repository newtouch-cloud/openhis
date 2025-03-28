using Newtouch.Common.Operator;
using Newtouch.HIS.Domain.DTO.PharmacyDrugStorage;
using Newtouch.HIS.Domain.Entity.PharmacyDrugStorage;
using Newtouch.HIS.Domain.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage
{
    public interface IPurchaseDmnService
    {
        string PurchaseSubmit(PurchaseEntity cgEntity, List<PurchaseDetailEntity> cgmxList, string orgId, OperatorModel user);
        List<PurchaseEntity> QueryPurchasebyId(string cgId, string orgId);
        List<PurchaseDetailVO> QueryPurchaseDetailbyId(string cgId, string orgId);
        List<PurchaseStoreDTO> QueryPurchaseStorebyId(string cgId, string orgId, string yfbmCode);
        List<BillStoreDTO> QueryBillStorebyId(string fph, string orgId, string yfbmCode);
    }
}
