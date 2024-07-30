using System.Linq;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineSupplierRepo : RepositoryBase<SysMedicineSupplierEntity>, ISysMedicineSupplierRepo
    {
        public SysMedicineSupplierRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据关键字返回供应商实体
        /// </summary>
        /// <param name="keyword">Gysdm、Gysmc、Mcsx、</param>
        /// <returns></returns>
        public SysMedicineSupplierEntity SelectMedicineSupplier(string keyword)
        {
            return this.IQueryable().Where(a => a.zt == "1" && (a.gysCode.Contains(keyword) || a.gysmc.Contains(keyword) || a.Mcsx.Contains(keyword))).FirstOrDefault();
        }
    }
}
