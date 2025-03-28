using Newtouch.Herp.Domain.Entity.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.IRepository.Purchase
{
    public interface IPurchaseDetailRepo
    {
        void SubmitForm(PurchaseDetailEntity entity, string keyValue);
        int InsertItem(PurchaseDetailEntity entity, string keyValue);
        int DeleteItem(string cgId, string orgId);
    }
}
