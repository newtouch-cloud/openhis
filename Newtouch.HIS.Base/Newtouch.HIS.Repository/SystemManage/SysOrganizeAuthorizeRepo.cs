using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    public class SysOrganizeAuthorizeRepo : RepositoryBase<SysOrganizeAuthorizeEntity>, ISysOrganizeAuthorizeRepo
    {
        public SysOrganizeAuthorizeRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

    }
}
