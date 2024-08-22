using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class DispensingallocationRepo : RepositoryBase<DispensingallocationEntity>, IDispensingallocationRepo
    {
        public DispensingallocationRepo(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
