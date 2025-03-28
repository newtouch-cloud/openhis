using FrameworkBase.MultiOrg.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysMedicineSupplierRepo
    {
        /// <summary>
        /// get Suppliers
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysMedicineSupplierVEntity> GetGysList(string keyword, string organizeId);
    }
}