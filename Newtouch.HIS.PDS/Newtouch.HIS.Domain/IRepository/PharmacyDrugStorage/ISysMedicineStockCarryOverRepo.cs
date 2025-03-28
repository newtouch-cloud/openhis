using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// xt_yp_kcjz
    /// </summary>
    public interface ISysMedicineStockCarryOverRepo : IRepositoryBase<SysMedicineStockCarryOverEntity>
    {
        /// <summary>
        /// 查询上一次结转的时间
        /// </summary>
        /// <returns></returns>
        CarryOverLastAccountPeriodAndCarrayTimeVO GetLastAccountPeriodAndCarrayTime();
    }
}
