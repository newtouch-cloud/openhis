using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrameworkBase.MultiOrg.Repository.SystemManage
{
    /// <summary>
    /// 角色
    /// </summary>
    public sealed class SysRoleRepo : RepositoryBase<SysRoleEntity>, ISysRoleRepo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysRoleRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 组织机构 角色列表 带 分页
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<SysRoleEntity> GetPagintionList(string orgId, Pagination pagination, string keyword = null)
        {
            var expression = ExtLinq.True<SysRoleEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.Name.Contains(keyword));
                expression = expression.Or(t => t.Code.Contains(keyword));
            }
            expression = expression.And(t => t.OrganizeId == orgId);
            return this.FindList(expression, pagination).OrderBy(p => p.px).ToList();
        }

    }
}
