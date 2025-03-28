using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
	/// <summary>
    /// 
    /// </summary>
    public class SysOrganizeApplicationRepo : RepositoryBase<SysOrganizeAppEntity>, ISysOrganizeAppRepository
    {
        public SysOrganizeApplicationRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}


