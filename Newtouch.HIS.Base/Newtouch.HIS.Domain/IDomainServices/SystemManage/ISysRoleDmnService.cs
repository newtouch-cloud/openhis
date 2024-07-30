using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 角色 DmnService
    /// </summary>
    public interface ISysRoleDmnService
    {
        /// <summary>
        /// 删除系统角色
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string keyValue);

        /// <summary>
        /// 提交新建、更新 实体 角色、角色权限
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="roleAuthorizeEntitys"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysRoleEntity roleEntity, List<SysRoleAuthorizeEntity> roleAuthorizeEntitys, string keyValue);

    }
}
