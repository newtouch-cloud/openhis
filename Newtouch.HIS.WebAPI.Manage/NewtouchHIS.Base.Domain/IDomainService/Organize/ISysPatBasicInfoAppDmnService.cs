using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.Organize
{
    public interface ISysPatBasicInfoAppDmnService : IScopedDependency
    {
        /// <summary>
        /// 获取病人性质列表
        /// </summary>
        /// <returns></returns>
        Task<List<SysPatientNatureEntity>> GetBRXZListAsync(string orgId);
    }
}
