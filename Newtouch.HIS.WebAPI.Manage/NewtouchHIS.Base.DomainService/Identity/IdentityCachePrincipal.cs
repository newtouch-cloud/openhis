using Microsoft.AspNetCore.Http;
using NewtouchHIS.Base.Domain;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Model.Authentication;
using System.Security.Claims;

namespace NewtouchHIS.Lib.Services
{
    /// <summary>
    /// 基于ClaimsPrincipal实现的用户信息接口。
    /// </summary>
    [Serializable]
    public class IdentityCachePrincipal : IIdentityCache
    {
        /// <summary>
        /// IHttpContextAccessor对象
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 如果IHttpContextAccessor.HttpContext?.User非空获取HttpContext的ClaimsPrincipal，否则获取线程的CurrentPrincipal
        /// </summary>
        protected ClaimsPrincipal Principal => _httpContextAccessor!.HttpContext!.User ?? (Thread.CurrentPrincipal as ClaimsPrincipal);

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public IdentityCachePrincipal(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public UserIdentity identity => new UserIdentity
        {
            TopOrganizeId = this.Principal!.FindFirst(IdentityClaimTypes.TopOrganizeId)!.Value,
            OrganizeId = this.Principal!.FindFirst(IdentityClaimTypes.OrganizeId)!.Value,
            UserId = this.Principal!.FindFirst(IdentityClaimTypes.UserId)!.Value,
            UserCode = this.Principal!.FindFirst(IdentityClaimTypes.UserCode)!.Value,
            UserName = this.Principal!.FindFirst(IdentityClaimTypes.UserName)!.Value,
            AppId = this.Principal!.FindFirst(IdentityClaimTypes.AppId)!.Value,
            Account = this.Principal!.FindFirst(IdentityClaimTypes.Account)!.Value
        };
        public void SetIdentityInfo(UserIdentity info, string appId)
        {
             
        }
    }
}
