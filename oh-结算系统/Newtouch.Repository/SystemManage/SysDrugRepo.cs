using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysDrugRepo : RepositoryBase<SysDrugEntity>, ISysDrugRepo
    {
        public SysDrugRepo(INewtouchDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}


