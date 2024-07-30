using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysRoleRepo : RepositoryBase<SysRoleEntity>, ISysRoleRepo
    {
        public SysRoleRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
