using Newtouch.HIS.Domain.Entity.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.Purchase
{
    public interface IPurchaseLocationRepo : IRepositoryBase<PurchaseLocationEntity>
    {
        //更新或新增
        string SubmitForm(PurchaseLocationEntity entity, string orgId, string keyValue);
        //更新配送点状态
        int LocationStateUpdate(string Id, string orgId, int psdzt);
    }
}
