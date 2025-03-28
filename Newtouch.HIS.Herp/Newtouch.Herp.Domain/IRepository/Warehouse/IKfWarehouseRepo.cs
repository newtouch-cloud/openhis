using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// 库房操作
    /// </summary>
    public interface IKfWarehouseRepo : IRepositoryBase<KfWarehouseEntity>
    {
        /// <summary>
        /// 获取目标兄弟或子库房
        /// </summary>
        /// <param name="kfId"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        List<KfWarehouseEntity> GetBrothersOrChildren(string kfId, string organizeId, string keyWord = "");

        /// <summary>
        /// 获取部门(父节点和兄弟节点)
        /// </summary>
        /// <param name="kfId"></param>
        /// <param name="keyWord"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<KfWarehouseEntity> GetParentOrBrothers(string kfId, string organizeId, string keyWord = "");
    }
}