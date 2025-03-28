using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 出入库方式
    /// </summary>
    public class WzCrkfsRepo : RepositoryBase<WzCrkfsEntity>, IWzCrkfsRepo
    {
        public WzCrkfsRepo(IBaseDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据ID删除出入库方式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteCrkfsById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return 0;
            var entity = FindEntity(p => p.Id == id);
            return entity == null ? 0 : Delete(entity);
        }
    }
}
