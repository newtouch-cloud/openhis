using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;

namespace Newtouch.Repository
{
    public class MRTemplateTCMDiagnosisRepo : RepositoryBase<MRTemplateTCMDiagnosisEntity>, IMRTemplateTCMDiagnosisRepo
    {
        public MRTemplateTCMDiagnosisRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
