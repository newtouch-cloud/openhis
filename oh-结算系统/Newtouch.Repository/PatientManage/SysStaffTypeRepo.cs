using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysStaffTypeRepo : RepositoryBase<SysStaffTypeEntity>, ISysStaffTypeRepo
    {
        public SysStaffTypeRepo(INewtouchDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}


