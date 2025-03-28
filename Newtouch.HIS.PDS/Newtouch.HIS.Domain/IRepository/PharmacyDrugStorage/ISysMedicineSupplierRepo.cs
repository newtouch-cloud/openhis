using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineSupplierRepo : IRepositoryBase<SysMedicineSupplierEntity>
    {
        /// <summary>
        /// 根据关键字返回供应商实体
        /// </summary>
        /// <param name="keyword">Gysdm、Gysmc、Mcsx、</param>
        /// <returns></returns>
        SysMedicineSupplierEntity SelectMedicineSupplier(string keyword);
    }
}
