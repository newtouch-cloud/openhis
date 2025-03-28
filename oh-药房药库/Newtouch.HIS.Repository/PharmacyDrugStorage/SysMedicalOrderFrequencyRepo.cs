using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicalOrderFrequencyRepo : RepositoryBase<SysMedicalOrderFrequencyEntity>, ISysMedicalOrderFrequencyRepo
    {
        public SysMedicalOrderFrequencyRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
