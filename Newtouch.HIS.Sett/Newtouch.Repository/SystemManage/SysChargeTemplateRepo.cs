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
    public class SysChargeTemplateRepo : RepositoryBase<SysChargeTemplateEntity>, ISysChargeTemplateRepo
    {
        public SysChargeTemplateRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}


