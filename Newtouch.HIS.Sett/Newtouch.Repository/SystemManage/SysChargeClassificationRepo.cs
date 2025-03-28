using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysChargeClassificationRepo : RepositoryBase<SysChargeClassificationVEntity>, ISysChargeClassificationRepo
    {
        public SysChargeClassificationRepo(INewtouchDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}
