using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;

namespace Newtouch.Repository
{
    public class TCMDiagnosisRepo : RepositoryBase<TCMDiagnosisEntity>, ITCMDiagnosisRepo
    {
        public TCMDiagnosisRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
