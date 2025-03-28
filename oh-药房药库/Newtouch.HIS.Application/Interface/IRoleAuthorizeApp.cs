using System.Collections.Generic;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 角色权限App
    /// </summary>
    public interface IRoleAuthorizeApp
    {
        ///// <summary>
        ///// 获取角色授权列表
        ///// </summary>
        ///// <param name="roleId"></param>
        ///// <returns></returns>
        //List<SysRoleAuthorizeEntity> GetValidList(string roleId);

        /// <summary>
        /// 访问权限验证
        /// </summary>
        /// <param name="roleIdList"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool ActionValidate(IList<string> roleIdList, string action);

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
