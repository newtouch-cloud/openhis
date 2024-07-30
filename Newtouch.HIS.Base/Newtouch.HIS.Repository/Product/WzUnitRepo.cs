using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 单位维护
    /// </summary>
    public class WzUnitRepo : RepositoryBase<WzUnitEntity>, IWzUnitRepo
    {
        public WzUnitRepo(IBaseDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// delete unit by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteUnitById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return 0;
            var entity = FindEntity(p => p.Id == id);
            return entity == null ? 0 : Delete(entity);
        }
    }
}
