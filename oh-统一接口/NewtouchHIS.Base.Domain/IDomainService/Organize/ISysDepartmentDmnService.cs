using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.Organize
{
    public interface ISysDepartmentDmnService : IScopedDependency
    {
        /// <summary>
        /// 根据Code获取科室名称
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<string> GetNameByCode(string code, string orgId);
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="keyword">筛选关键字</param>
        Task<List<SysDepartmentEntity>> GetList(string keyword = null);
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="keyword">筛选关键字</param>
        Task<List<SysDepartmentEntity>> GetListByOrg(string organizeId);
    }
}
