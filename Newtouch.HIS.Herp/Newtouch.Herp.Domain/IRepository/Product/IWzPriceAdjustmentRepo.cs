using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// 物资调价
    /// </summary>
    public interface IWzPriceAdjustmentRepo : IRepositoryBase<WzPriceAdjustmentEntity>
    {
        /// <summary>
        /// 获取未执行列表
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<WzPriceAdjustmentEntity> GetUnexecutedList(string productId, string organizeId);
    }
}