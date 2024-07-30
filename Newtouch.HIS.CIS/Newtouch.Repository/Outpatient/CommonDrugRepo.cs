using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class CommonDrugRepo : RepositoryBase<CommonDrugEntity>, ICommonDrugRepo
    {
        public CommonDrugRepo(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
