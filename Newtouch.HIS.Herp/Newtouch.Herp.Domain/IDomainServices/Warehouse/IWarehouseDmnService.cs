using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// 库房操作
    /// </summary>
    public interface IWarehouseDmnService
    {
        /// <summary>
        /// insert new warehouse information
        /// </summary>
        /// <param name="wzEntity"></param>
        /// <param name="deptList"></param>
        /// <param name="userList"></param>
        void InsertWarehouse(KfWarehouseEntity wzEntity, List<RelWarehouseUserEntity> userList, List<RelWarehouseDeptEntity> deptList);

        /// <summary>
        /// update warehouse information
        /// </summary>
        /// <param name="wzEntity"></param>
        /// <param name="deptList"></param>
        /// <param name="userList"></param>
        void UpdateWarehouse(KfWarehouseEntity wzEntity, List<RelWarehouseUserEntity> userList, List<RelWarehouseDeptEntity> deptList);

        /// <summary>
        /// 获取库房信息
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="organizeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        IList<VKfWarehouseEntity> QueryWarehouseInfo(string keyWord, string organizeId, string zt = "1");

        /// <summary>
        /// 删除库房
        /// </summary>
        /// <param name="id">库房Id</param>
        void DeleteWarehouse(string id);

        /// <summary>
        /// 获取库房管理员信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VKfUserInfoEntity> GetKfUserInfo(string userId, string organizeId);

        /// <summary>
        /// 获取库房同步物资
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="warehouseId">库房ID</param>
        /// <param name="wzlb">物资类别</param>
        /// <param name="keyWord">查询关键字</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VSyncProductEntity> GetSyncProductList(Pagination pagination, string warehouseId, string wzlb, string keyWord, string organizeId);

        /// <summary>
        /// 获取库房物资
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <param name="lb"></param>
        /// <param name="kzbz"></param>
        /// <param name="zt"></param>
        /// <param name="organizeId"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        IList<VWarehouseProductEntity> GetWarehouseProducts(Pagination pagination, string keyWord, string lb,
           string kzbz, string zt, string organizeId, string warehouseId);
    }
}