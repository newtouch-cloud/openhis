using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Base.Domain.Organize
{
    public interface ISysRoleDmnService : IScopedDependency
    {
        /// <summary>
        /// 获取用户已授权的角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysRoleEntity>> GetUserRoleList(string userId, string orgId);
        /// <summary>
        /// 获取指定系统用户已授权角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<List<SysRoleEntity>> GetUserRoleList(string userId, string orgId, string db);
        /// <summary>
        /// 校验用户是否是管理员
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<bool> IsHospAdmin(string userId, string orgId);
        /// <summary>
        /// 删除系统角色
        /// </summary>
        /// <param name="keyValue"></param>
        Task DeleteForm(string keyValue);

        /// <summary>
        /// 提交新建、更新 实体 角色、角色权限
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="roleAuthorizeEntitys"></param>
        /// <param name="keyValue"></param>
        Task SubmitForm(SysRoleEntity roleEntity, List<Entity.SysRoleAuthorizeEntity> roleAuthorizeEntitys, string keyValue,string UserCode, string orgId);
        /// <summary>
        /// 组织机构 角色列表 带 分页
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<SysRoleEntity>> GetPagintionList(string orgId, string? keyword = null);

        /// <summary>
        /// 获取角色实体
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<SysRoleEntity> GetForm(string keyword);
        /// <summary>
        /// 系统角色列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysRoleEntity>> GetRoleList(string orgId);
    }
}
