using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineClassificationRepo : RepositoryBase<SysMedicineClassificationEntity>, ISysMedicineClassificationRepo
    {
        public SysMedicineClassificationRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
