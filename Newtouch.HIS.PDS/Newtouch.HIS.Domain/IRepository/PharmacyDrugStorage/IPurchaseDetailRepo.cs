using Newtouch.HIS.Domain.Entity.PharmacyDrugStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.PharmacyDrugStorage
{
    public interface IPurchaseDetailRepo
    {
        void SubmitForm(PurchaseDetailEntity entity,  string keyValue);
        int InsertItem(PurchaseDetailEntity entity, string keyValue);
        int DeleteItem(string cgId,string orgId);
    }
}
