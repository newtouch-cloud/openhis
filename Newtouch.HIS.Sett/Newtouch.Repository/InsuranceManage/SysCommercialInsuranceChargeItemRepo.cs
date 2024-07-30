using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysCommercialInsuranceChargeItemRepo : RepositoryBase<SysCommercialInsuranceChargeItemEntity>, ISysCommercialInsuranceChargeItemRepo
    {
        public SysCommercialInsuranceChargeItemRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
