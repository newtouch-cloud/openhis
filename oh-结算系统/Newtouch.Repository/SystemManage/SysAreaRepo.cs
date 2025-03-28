using System.Linq;
using Newtouch.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysAreaRepo : RepositoryBase<SysAreaEntity>, ISysAreaRepo
    {
        public SysAreaRepo(INewtouchDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}


