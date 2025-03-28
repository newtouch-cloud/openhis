using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
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
    }
}
