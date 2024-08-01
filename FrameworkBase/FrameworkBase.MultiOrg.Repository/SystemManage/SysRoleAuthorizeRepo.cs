using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;

namespace FrameworkBase.MultiOrg.Repository.SystemManage
{
    /// <summary>
    /// 角色菜单授权
    /// </summary>
    public sealed class SysRoleAuthorizeRepo : RepositoryBase<SysRoleAuthorizeEntity>, ISysRoleAuthorizeRepo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysRoleAuthorizeRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

    }
}
