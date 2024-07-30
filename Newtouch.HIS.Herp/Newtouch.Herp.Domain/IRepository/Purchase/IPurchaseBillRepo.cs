using Newtouch.Herp.Domain.DTO.InputDto.Purchase;
using Newtouch.Herp.Domain.Entity.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.IRepository.Purchase
{
    public interface IPurchaseBillRepo
    {
        int InsertItem(PurchaseMainYY132 entity,string orgId);
    }
}
