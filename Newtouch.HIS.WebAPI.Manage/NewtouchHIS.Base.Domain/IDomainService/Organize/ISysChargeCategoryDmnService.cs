using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.Organize
{
    public interface ISysChargeCategoryDmnService : IScopedDependency
    {
        /// <summary>
        /// 获取有效
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<SysChargeCategoryVEntity>> GetList(string orgId, string dllbs = "", string zt = "");
    }
}
