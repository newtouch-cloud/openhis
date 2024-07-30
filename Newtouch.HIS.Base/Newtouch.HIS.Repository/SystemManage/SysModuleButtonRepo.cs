using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysModuleButtonRepo : RepositoryBase<SysModuleButtonEntity>, ISysModuleButtonRepo
    {
        public SysModuleButtonRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}


