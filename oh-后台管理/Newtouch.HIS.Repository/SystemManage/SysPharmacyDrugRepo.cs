using Newtouch.HIS.Domain;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 药房部门药品
    /// </summary>
    public class SysPharmacyDrugRepo : RepositoryBase<SysPharmacyDepartmentOpenMedicineEntity>, ISysPharmacyDrugRepo
    {
        public SysPharmacyDrugRepo(IBaseDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}