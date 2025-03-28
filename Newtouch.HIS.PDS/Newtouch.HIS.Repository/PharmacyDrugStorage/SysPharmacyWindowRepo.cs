using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPharmacyWindowRepo : RepositoryBase<SysPharmacyWindowEntity>, ISysPharmacyWindowRepo
    {
        public SysPharmacyWindowRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
