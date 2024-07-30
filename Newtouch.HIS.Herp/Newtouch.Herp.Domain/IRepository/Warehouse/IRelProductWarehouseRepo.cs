using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// 库房物资
    /// </summary>
    public interface IRelProductWarehouseRepo : IRepositoryBase<RelProductWarehouseEntity>
    {
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int BatchDelete(List<RelProductWarehouseEntity> list);
    }
}
