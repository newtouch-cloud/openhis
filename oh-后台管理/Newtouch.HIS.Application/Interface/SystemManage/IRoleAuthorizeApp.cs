using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 角色权限App
    /// </summary>
    public interface IRoleAuthorizeApp
    {
        /// <summary>
        /// 获取角色授权列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<SysRoleAuthorizeEntity> GetValidList(string roleId);

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="moduleId"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool ActionValidate(IList<string> roleId, string moduleId, string action);
    }
}
