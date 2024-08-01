using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace FrameworkBase.MultiOrg.Domain.IRepository
{
    /// <summary>
    /// 角色
    /// </summary>
    public interface ISysRoleRepo : IRepositoryBase<SysRoleEntity>
    {
        /// <summary>
        /// 组织机构 角色列表 带 分页
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<SysRoleEntity> GetPagintionList(string orgId, Pagination pagination, string keyword = null);

    }
}
