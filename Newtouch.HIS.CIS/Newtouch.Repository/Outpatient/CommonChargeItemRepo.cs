using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class CommonChargeItemRepo : RepositoryBase<CommonChargeItemEntity>, ICommonChargeItemRepo
    {
        public CommonChargeItemRepo(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
