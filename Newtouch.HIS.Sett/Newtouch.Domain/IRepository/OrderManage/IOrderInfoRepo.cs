using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOrderInfoRepo : IRepositoryBase<OrderInfoEntity>
    {
        void SubmitInfo(OrderInfoEntity entity, string keyValue = null);
        

    }
}
