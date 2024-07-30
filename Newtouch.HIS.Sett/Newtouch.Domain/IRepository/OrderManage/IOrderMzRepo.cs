using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOrderMzRepo : IRepositoryBase<OrderMzEntity>
    {
        void SubmitInfo(OrderMzEntity entity, string keyValue = null);
        

    }
}
