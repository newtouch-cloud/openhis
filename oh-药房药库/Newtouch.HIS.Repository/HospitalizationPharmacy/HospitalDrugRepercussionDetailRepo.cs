using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class HospitalDrugRepercussionDetailRepo : RepositoryBase<HospitalDrugRepercussionDetailEntity>, IHospitalDrugRepercussionDetailRepo
    {
        public HospitalDrugRepercussionDetailRepo(IDefaultDatabaseFactory databaseFactory): base(databaseFactory)
        {
        }
    }
}
