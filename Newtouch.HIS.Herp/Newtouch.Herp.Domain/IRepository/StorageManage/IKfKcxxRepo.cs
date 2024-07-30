using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// 库存信息
    /// </summary>
    public interface IKfKcxxRepo : IRepositoryBase<KfKcxxEntity>
    {

        /// <summary>
        /// 查询库存信息
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<KfKcxxEntity> SelectData(string warehouseId, string productId);

        /// <summary>
        /// 修改库存状态
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="ph"></param>
        /// <param name="pc"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        int UpdateZt(string productId, string ph, string pc, string zt);
    }
}