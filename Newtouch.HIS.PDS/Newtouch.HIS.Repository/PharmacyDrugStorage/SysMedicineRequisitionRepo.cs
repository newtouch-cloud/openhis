using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// xt_yp_sld
    /// </summary>
    public class SysMedicineRequisitionRepo : RepositoryBase<SysMedicineRequisitionEntity>, ISysMedicineRequisitionRepo
    {
        public SysMedicineRequisitionRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
