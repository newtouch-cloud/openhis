using System.Collections.Generic;

namespace FrameworkBase.Application.Interface
{
    /// <summary>
    /// 角色权限App
    /// </summary>
    public interface IRoleAuthorizeApp
    {
        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="roleIdList"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool ActionValidate(IList<string> roleIdList, string action);

        /// <summary>
        /// 权限验证（要求必须通过菜单点击方式 打卡页面）
        /// </summary>
        /// <param name="roleIdList"></param>
        /// <param name="moduleId"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool ActionValidate(IList<string> roleIdList, string moduleId, string action);

    }
}
