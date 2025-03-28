using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOrderZyRepo : IRepositoryBase<OrderZyEntity>
    {
        void SubmitInfo(OrderZyEntity entity, string keyValue = null);
        

    }
}
