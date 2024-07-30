using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysUserLogOnRepo : RepositoryBase<SysUserLogOnEntity>, ISysUserLogOnRepo
    {
        public SysUserLogOnRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
