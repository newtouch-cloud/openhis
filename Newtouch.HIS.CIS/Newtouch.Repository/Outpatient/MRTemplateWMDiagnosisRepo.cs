using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class MRTemplateWMDiagnosisRepo : RepositoryBase<MRTemplateWMDiagnosisEntity>, IMRTemplateWMDiagnosisRepo
    {
        public MRTemplateWMDiagnosisRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
