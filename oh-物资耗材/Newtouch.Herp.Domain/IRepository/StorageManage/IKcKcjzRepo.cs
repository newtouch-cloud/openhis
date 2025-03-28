using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.ValueObjects;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// 库存结转
    /// </summary>
    public interface IKcKcjzRepo : IRepositoryBase<KcKcjzEntity>
    {
        /// <summary>
        /// 获取历史结转时间
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SelectVo> GetlsjzDateTime(string warehouseId, string organizeId);

        /// <summary>
        /// 获取最近一次结转信息
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        KcKcjzEntity GetLastJzData(string warehouseId, string organizeId);
    }
}