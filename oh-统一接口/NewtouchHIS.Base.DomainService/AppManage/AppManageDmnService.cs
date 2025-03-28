using Mapster;
using NewtouchHIS.Base.Domain;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Services.AuthSecurity;
using static NewtouchHIS.Lib.Base.BaseEnum;

namespace NewtouchHIS.Base.DomainService
{
    public class AppManageDmnService : BaseDmnService<SysAuthAppEntity>, IAppManageDmnService
    {

        #region Query
        public async Task<AuthAppVO> GetAppInfo(AppFriendAuthKeyRequest request)
        {
            var app = await GetFirstOrDefault(p => p.AppId == request.AppId && p.zt == "1");
            if (app == null)
            {
                throw new FailedException($"未找到目标应用{request.AppId}");
            }
            return app.Adapt<AuthAppVO>();
        }
        public async Task<List<AuthAppVO>> GetRegAppList()
        {
            var app = await GetByWhere(p => p.zt == "1");
            return app.Adapt<List<AuthAppVO>>();
        }
        /// <summary>
        /// 根据应用好友授权信息生成key
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> AppKeyGen(AppFriendAuthKeyRequest request)
        {
            var keyModel = await GetAppFriend(request);
            return AuthSecurityServices.AppAuthKeyGen(keyModel.ToJson());
        }
        /// <summary>
        /// 应用好友授权信息 AppId 申请授权 
        /// 查找 Friend 应用的好友关系中是否包含AppId
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="FailedException"></exception>
        public async Task<AppFriendAuthKey> GetAppFriend(AppFriendAuthKeyRequest request)
        {
            AppFriendAuthKey authKey = new AppFriendAuthKey();
            var app = await GetFirstOrDefault(p => p.AppId == request.AppId && p.zt == "1");
            if (app == null)
            {
                throw new FailedException($"未找到目标应用{request.AppId}");
            }
            if (!string.IsNullOrWhiteSpace(request.FriendAppId))
            {
                var friendApp = await GetFirstOrDefault(p => p.AppId == request.FriendAppId && p.zt == "1");
                if (friendApp == null)
                {
                    throw new FailedException($"未找到申请授权的应用{request.FriendAppId}");
                }
                var findFriendAuth = await GetFirstOrDefaultWithAttr<SysAuthAppFriendEntity>(p => p.FriendId == friendApp.AuthId && p.AppAuthId == app.AuthId && p.zt == "1");
                if (findFriendAuth == null)
                {
                    throw new FailedException($"目标应用尚未对{request.FriendAppId}开放授权，请联系管理员");
                }
                authKey.FriendAppId = friendApp.AppId;
                authKey.FriendId = friendApp.AuthId;
                authKey.AuthLevs = findFriendAuth.AuthLevs;
            }
            authKey.AuthId = app.AuthId;
            authKey.AppId = request.AppId;
            authKey.AppName = app.AppName;
            return authKey;
        }

        #endregion

        #region 增删改
        /// <summary>
        /// 添加授权应用
        /// </summary>
        /// <param name="authApp"></param>
        /// <returns></returns>
        public async Task<SysAuthAppEntity> AddAuthApp(AuthAppVO authApp)
        {
            if (string.IsNullOrEmpty(authApp.AppName) || string.IsNullOrWhiteSpace(authApp.AppId) || !authApp.AuthType.HasValue)
            {
                throw new FailedException("关键信息不可为空（应用Id、应用名称、授权类型）");
            }
            var existsApp = await GetByWhere(p => (p.AuthId == authApp.AuthId || p.AppName == authApp.AppName) && p.zt == "1");
            if (existsApp != null && existsApp!.Count > 0)
            {
                throw new FailedException(existsApp.FirstOrDefault()!.AppId == authApp.AppId ? "应用Id已被注册，请更换Id" : "应用名称已被注册，请更换");
            }
            if (authApp.AuthType > 0 && string.IsNullOrWhiteSpace(Enum.GetName(typeof(AuthTypeEnum), authApp.AuthType)))
            {
                throw new FailedException("系统注册失败，请联系管理员");
            }
            SysAuthAppEntity sysAuthApp = new SysAuthAppEntity();
            sysAuthApp.AppId = authApp.AppId;
            sysAuthApp.AuthId = System.Guid.NewGuid().ToString();
            sysAuthApp.AppName = authApp.AppName;
            sysAuthApp.Domain = authApp.Domain ?? string.Empty;
            sysAuthApp.AuthType = authApp.AuthType > 0 ? authApp.AuthType : (int)AuthTypeEnum.Web;
            sysAuthApp.Host = authApp.Host ?? string.Empty;
            sysAuthApp.CreateTime = DateTime.Now;
            sysAuthApp.CreatorCode = "chl";
            var result = await Add(sysAuthApp);
            if (result == 0)
            {
                throw new FailedException("系统注册失败，请联系管理员");
            }
            var newApp = await GetByWhere(p => p.AuthId == sysAuthApp.AuthId);
            //var newAppVo = _mapper.Map<AuthAppVO>(newApp.FirstOrDefault());
            return newApp.FirstOrDefault();
        }

        public async Task<bool> DelAuthApp(AuthAppVO authapp)
        {
            if (authapp == null || string.IsNullOrWhiteSpace(authapp.AuthId))
            {
                throw new FailedException("请传入系统授权Id");
            }
            bool result = false;
            var ety = await GetByWhere(p => p.AuthId == authapp.AuthId && p.zt == "1");
            if (ety != null && ety.Count == 1 && ety.FirstOrDefault() != null)
            {
                var e = ety.FirstOrDefault();
                e.zt = "0";
                e.LastModifyTime = DateTime.Now;
                e.LastModifierCode = "chl";
                result = await Update(e);
            }
            else
            {
                throw new FailedException("系统信息不存在或数据异常");
            }
            return result;
        }

        #endregion
    }
}


