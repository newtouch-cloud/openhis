using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class UsedDiagnosisRepo : RepositoryBase<UsedDiagnosisEntity>, IUsedDiagnosisRepo
    {
        public UsedDiagnosisRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

       
    }
}
