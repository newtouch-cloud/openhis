using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class HospAccountingPlanReplacementItemRepo : RepositoryBase<HospAccountingPlanReplacementItemEntity>, IHospAccountingPlanReplacementItemRepo
    {
        public HospAccountingPlanReplacementItemRepo(INewtouchDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
