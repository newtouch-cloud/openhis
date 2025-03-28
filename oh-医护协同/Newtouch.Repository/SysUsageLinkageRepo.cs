using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.Repository
{

    /// <summary>
    /// 系统用法联动
    /// </summary>
    public class SysUsageLinkageRepo : RepositoryBase<SysUsageLinkageEntity>, ISysUsageLinkageRepo
    {
        public SysUsageLinkageRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

    }
}
