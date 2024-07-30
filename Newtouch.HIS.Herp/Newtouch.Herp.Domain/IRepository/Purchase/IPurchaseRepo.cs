using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.IRepository.Purchase
{
    public interface IPurchaseRepo
    {
        IList<PurchaseEntity> GetPurchaseGridJson(Pagination pagination, DateTime kssj, DateTime jssj, string OrganizeId, int ddzt);
        void PurchaseDelete(string cgId, string orgId);
        void PurchaseStateUpdate(string cgId, int ddzt, string orgId);
        void PurchaseDdbhUpdate(string cgId, string ddbh, string orgId);
        string SubmitForm(PurchaseEntity entity, string keyValue);
    }
}
