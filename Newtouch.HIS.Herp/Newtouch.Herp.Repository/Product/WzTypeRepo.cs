using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 物资类别
    /// </summary>
    public class WzTypeRepo : RepositoryBase<WzTypeEntity>, IWzTypeRepo
    {
        public WzTypeRepo(IBaseDatabaseFactory databaseFactory) : base(databaseFactory)
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
