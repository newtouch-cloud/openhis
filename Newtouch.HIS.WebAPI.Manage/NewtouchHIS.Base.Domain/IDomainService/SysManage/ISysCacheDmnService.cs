using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Base.Domain.IDomainService
{
    /// <summary>
    /// 系统缓存
    /// </summary>
    public interface ISysCacheDmnService : ISingletonDependency
    {
        /// <summary>
        /// 用户身份信息缓存
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        Task<BusResult<UserIdentity>> GetUserDataCache(string accessToken);
    }
}
