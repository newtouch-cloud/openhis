using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// xt_yp_sldmx
    /// </summary>
    public class SysMedicineRequisitionDetailRepo : RepositoryBase<SysMedicineRequisitionDetailEntity>, ISysMedicineRequisitionDetailRepo
    {
        public SysMedicineRequisitionDetailRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
