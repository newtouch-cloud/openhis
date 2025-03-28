using Newtouch.Herp.Domain.Entity.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.IRepository.Purchase
{
    public interface IReturnedDetailRepo
    {
        //void SubmitForm(ReturnedDetailEntity entity, string keyValue);
        int InsertItem(ReturnedDetailEntity entity, string keyValue);
        int DeleteItem(string thId, string orgId);
    }
}
