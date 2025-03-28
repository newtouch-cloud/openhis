using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.IDomainService
{
    public interface IAppManageDmnService : IScopedDependency
    {
        /// <summary>
        /// 获取注册应用列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<AuthAppVO>> GetRegAppList();
        /// <summary>
        /// 获取应用注册信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AuthAppVO> GetAppInfo(AppFriendAuthKeyRequest request);
        /// <summary>
        /// 根据应用授权信息生成key
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> AppKeyGen(AppFriendAuthKeyRequest request);
        /// <summary>
        /// 应用好友授权信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AppFriendAuthKey> GetAppFriend(AppFriendAuthKeyRequest request);
        #region 实体操作
        Task<SysAuthAppEntity> AddAuthApp(AuthAppVO authApp);
        Task<bool> DelAuthApp(AuthAppVO authapp);
        #endregion
    }
}
