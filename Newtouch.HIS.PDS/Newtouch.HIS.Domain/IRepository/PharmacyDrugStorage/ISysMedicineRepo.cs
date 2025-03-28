using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// V_S_xt_yp
    /// </summary>
    public interface ISysMedicineRepo : IRepositoryBase<SysMedicineVEntity>
    {
        /// <summary>
        /// 获取一个药品对象
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        SysMedicineVEntity GetMedicineByCode(string orgId, string ypCode);

        /// <summary>
        /// 获取一个药品集合
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<SysMedicineVEntity> GetMedicineListByOrg(string orgId);
    }
}
