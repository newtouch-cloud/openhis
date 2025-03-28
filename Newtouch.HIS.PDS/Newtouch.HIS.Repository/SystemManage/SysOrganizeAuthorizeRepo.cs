using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysOrganizeAuthorizeRepo : RepositoryBase<SysOrganizeAuthorizeEntity>, ISysOrganizeAuthorizeRepo
    {
        public SysOrganizeAuthorizeRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
