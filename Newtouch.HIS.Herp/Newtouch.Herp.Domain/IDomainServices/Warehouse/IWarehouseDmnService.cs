using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// �ⷿ����
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
        /// ��ȡ�ⷿ��Ϣ
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="organizeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        IList<VKfWarehouseEntity> QueryWarehouseInfo(string keyWord, string organizeId, string zt = "1");

        /// <summary>
        /// ɾ���ⷿ
        /// </summary>
        /// <param name="id">�ⷿId</param>
        void DeleteWarehouse(string id);

        /// <summary>
        /// ��ȡ�ⷿ����Ա��Ϣ
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VKfUserInfoEntity> GetKfUserInfo(string userId, string organizeId);

        /// <summary>
        /// ��ȡ�ⷿͬ������
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="warehouseId">�ⷿID</param>
        /// <param name="wzlb">�������</param>
        /// <param name="keyWord">��ѯ�ؼ���</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VSyncProductEntity> GetSyncProductList(Pagination pagination, string warehouseId, string wzlb, string keyWord, string organizeId);

        /// <summary>
        /// ��ȡ�ⷿ����
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