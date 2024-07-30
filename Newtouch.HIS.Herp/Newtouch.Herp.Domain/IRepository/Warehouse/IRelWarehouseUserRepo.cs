using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// 库房管理员操作
    /// </summary>
    public interface IRelWarehouseUserRepo : IRepositoryBase<RelWarehouseUserEntity>
    {
        /// <summary>
        /// get RelWarehouseUserEntity list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="organizeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        List<RelWarehouseUserEntity> GetListById(string id, string organizeId, string zt = "1");

        /// <summary>
        /// get RelWarehouseUserEntity list
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        List<RelWarehouseUserEntity> GetListByWarehouseId(string warehouseId, string organizeId, string zt = "1");
    }
}