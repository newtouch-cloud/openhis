using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Base.Domain.Organize
{
    public interface ISysUserVDmnService : IScopedDependency
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<BusResult<SysUserVEntity>> UserLogin(LoginBO login);
        /// <summary>
        /// 登录成功身份认证
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="staffOrganizeId"></param>
        /// <returns></returns>
        Task<OperatorModel?> BuildLoginStatusOpr(string userId, string staffOrganizeId = null);
    }
}
