using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
	/// <summary>
    /// 
    /// </summary>
    public class SysRoleAuthorizeRepo : RepositoryBase<SysRoleAuthorizeEntity>, ISysRoleAuthorizeRepo
    {
        public SysRoleAuthorizeRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}


