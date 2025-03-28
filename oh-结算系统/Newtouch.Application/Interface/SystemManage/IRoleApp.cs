using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 角色App
    /// </summary>
    public interface IRoleApp
    {
        /// <summary>
        /// 组织机构 角色列表 带 分页
        /// </summary>
        /// <param name="topOrganizeId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<SysRoleEntity> GetPagintionList(string orgId, Pagination pagination, string keyword = null);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysRoleEntity GetForm(string keyValue);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string keyValue);

        /// <summary>
        /// 提交保存
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="permissionIds"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysRoleEntity roleEntity, string[] permissionIds, string keyValue);


    }
}
