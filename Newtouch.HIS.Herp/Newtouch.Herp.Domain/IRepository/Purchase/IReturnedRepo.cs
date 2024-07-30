using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.IRepository.Purchase
{
    public interface IReturnedRepo
    {
        string SubmitForm(ReturnedEntity entity, string keyValue);
        IList<ReturnedEntity> GetReturnedGridJson(Pagination pagination, DateTime kssj, DateTime jssj, string OrganizeId);
        void PurchaseReturnDelete(string thId, string orgId);
        void PurchaseReturnStateUpdate(string thId, int thdzt, string orgId);
        void PurchaseDdbhUpdate(string thId, string ddbh, string orgId);
    }
}
