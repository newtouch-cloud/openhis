using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FrameworkBase.MultiOrg.Repository.SystemManage
{
    /// <summary>
    /// 用户角色关联关系
    /// </summary>
    public sealed class SysUserRoleRepo : RepositoryBase<SysUserRoleEntity>, ISysUserRoleRepo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysUserRoleRepo(IDefaultDatabaseFactory databaseFactory) 
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取UserId关联RoleId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<string> GetRoleIdListByUserId(string userId)
        {
            var sql = "select RoleId from Sys_UserRole(nolock) where UserId = @userId and zt = '1'";
            return this.FindList<string>(sql, new SqlParameter[] {
                new SqlParameter("@userId",userId)
            });
        }

    }
}
