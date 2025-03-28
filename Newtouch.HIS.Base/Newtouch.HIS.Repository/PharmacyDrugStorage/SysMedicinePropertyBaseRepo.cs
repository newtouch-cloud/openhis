using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicinePropertyBaseRepo : RepositoryBase<SysMedicinePropertyBaseEntity>, ISysMedicinePropertyBaseRepo
    {
        public SysMedicinePropertyBaseRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
