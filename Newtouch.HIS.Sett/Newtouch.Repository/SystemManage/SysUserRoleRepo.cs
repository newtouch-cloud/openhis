using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysUserRoleRepo : RepositoryBase<SysUserRoleEntity>, ISysUserRoleRepo
    {
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


