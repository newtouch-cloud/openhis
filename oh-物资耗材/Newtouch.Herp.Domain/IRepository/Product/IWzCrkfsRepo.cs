using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// 出入库方式
    /// </summary>
    public interface IWzCrkfsRepo : IRepositoryBase<WzCrkfsEntity>
    {
        /// <summary>
        /// 根据ID删除出入库方式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteCrkfsById(string id);

        /// <summary>
        /// 查询出入库方式
        /// </summary>
        /// <param name="crkbz"></param>
        /// <returns></returns>
        List<WzCrkfsEntity> SelectData(string crkbz);
    }
}