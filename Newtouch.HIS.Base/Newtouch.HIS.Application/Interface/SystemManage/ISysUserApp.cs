using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysUserApp
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        SysUserEntity CheckLogin(string username, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userPassword"></param>
        /// <param name="keyValue"></param>
        void RevisePassword(string userPassword, string keyValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userEntity"></param>
        void UpdateUserZT(SysUserEntity userEntity);

        /// <summary>
        /// 为组织机构创建一个系统管理员 admin
        /// </summary>
        /// <param name="userPassword"></param>
        void CreateDefaultAdminToOrg(string topOrgId, string userPassword);

        /// <summary>
        /// 微软SSO登录(邮箱)
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="organizeId">机构ID</param>
        /// <returns></returns>
        SysUserEntity MsSsoLogin(string userId, string organizeId);
    }

}
