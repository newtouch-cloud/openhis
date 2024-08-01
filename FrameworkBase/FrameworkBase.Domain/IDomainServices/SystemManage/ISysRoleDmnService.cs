using FrameworkBase.Domain.Entity;

namespace FrameworkBase.Domain.IDomainServices
{
    /// <summary>
    /// 角色相关
    /// </summary>
    public interface ISysRoleDmnService
    {
        /// <summary>
        /// 提交角色 关联菜单
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="permissionIds"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysRoleEntity roleEntity, string[] permissionIds, string keyValue);

        /// <summary>
        /// 删除系统角色
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string keyValue);

    }
}
