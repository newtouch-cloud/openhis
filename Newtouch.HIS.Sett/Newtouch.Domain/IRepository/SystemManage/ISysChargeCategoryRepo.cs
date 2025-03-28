using FrameworkBase.MultiOrg.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 收费大类
    /// </summary>
    public interface ISysChargeCategoryRepo : IRepositoryBase<SysChargeCategoryVEntity>
    {
        /// <summary>
        /// 获取收费大类列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dllbs"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        IList<SysChargeCategoryVEntity> GetList(string orgId, string dllbs = "", string zt = "");

        /// <summary>
        /// 收取所有收费大类（带缓存）
        /// </summary>
        /// <returns></returns>
        IList<SysChargeCategoryVEntity> GetLazyList(string orgId);

    }
}
