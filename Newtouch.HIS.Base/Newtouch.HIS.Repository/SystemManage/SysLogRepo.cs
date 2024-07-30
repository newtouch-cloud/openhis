using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
	/// <summary>
    /// 
    /// </summary>
    public class SysLogRepo : RepositoryBase<SysLogEntity>, ISysLogRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysLogRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}


