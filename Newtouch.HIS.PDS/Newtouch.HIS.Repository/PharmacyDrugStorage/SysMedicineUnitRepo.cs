using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineUnitRepo : RepositoryBase<SysMedicineUnitEntity>, ISysMedicineUnitRepo
    {
        public SysMedicineUnitRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
