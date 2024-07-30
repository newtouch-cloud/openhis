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
    public interface IPurchaseBillDetailRepo
    {
        int InsertItem(OutputStructYY004 entity, string orgId);
    }
}
