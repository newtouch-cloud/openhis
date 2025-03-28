using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class HospAccountingPlanDoctorRepo : RepositoryBase<HospAccountingPlanDoctorEntity>, IHospAccountingPlanDoctorRepo
    {
        public HospAccountingPlanDoctorRepo(INewtouchDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
