using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class HospExecuteMedicalOrderReduceBurdenItemRepo : RepositoryBase<HospExecuteMedicalOrderReduceBurdenItemEntity>, IHospExecuteMedicalOrderReduceBurdenItemRepo
    {
        public HospExecuteMedicalOrderReduceBurdenItemRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}


