using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO.PharmacyDrugStorage;
using Newtouch.HIS.Domain.Entity.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.Purchase
{
    public interface IPurchaseBillRepo
    {
        int InsertItem(PurchaseMainYY019 entity, string orgId);
        IList<PurchaseBillEntity> GetBillStoreGridJson(Pagination pagination, DateTime kssj, DateTime jssj, string fph, string OrganizeId, string fpzt);
       
    }
}
