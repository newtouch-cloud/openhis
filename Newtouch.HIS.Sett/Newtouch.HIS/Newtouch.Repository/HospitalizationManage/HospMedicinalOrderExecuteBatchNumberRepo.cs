using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class HospMedicinalOrderExecuteBatchNumberRepo : RepositoryBase<HospMedicinalOrderExecuteBatchNumberEntity>, IHospMedicinalOrderExecuteBatchNumberRepo
    {
        public HospMedicinalOrderExecuteBatchNumberRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
