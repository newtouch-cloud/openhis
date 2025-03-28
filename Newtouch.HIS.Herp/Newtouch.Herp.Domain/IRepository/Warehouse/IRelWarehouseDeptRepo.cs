using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// 库房科室
    /// </summary>
    public interface IRelWarehouseDeptRepo : IRepositoryBase<RelWarehouseDeptEntity>
    {

        /// <summary>
        /// get RelWarehouseDeptEntity list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="organizeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        List<RelWarehouseDeptEntity> GetListById(string id, string organizeId, string zt = "1");

        /// <summary>
        /// get RelWarehouseDeptEntity list
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        List<RelWarehouseDeptEntity> GetListByWarehouseId(string warehouseId, string organizeId, string zt = "1");

        /// <summary>
        /// get RelWarehouseDeptEntity list
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<RelWarehouseDeptEntity> GetList(string warehouseId, string organizeId, string keyword);
    }
}