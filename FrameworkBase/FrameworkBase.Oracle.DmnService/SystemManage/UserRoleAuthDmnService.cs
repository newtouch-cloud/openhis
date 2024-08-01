using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using FrameworkBase.Domain.IDomainServices;
using FrameworkBase.Infrastructure;
using FrameworkBase.Domain.Entity;
using Oracle.ManagedDataAccess.Client;

namespace FrameworkBase.Oracle.DmnService
{
    /// <summary>
    /// 用户角色权限
    /// </summary>
    public sealed class UserRoleAuthDmnService : DmnServiceBase, IUserRoleAuthDmnService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public UserRoleAuthDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取用户已授权的角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<SysRoleEntity> GetUserRoleList(string userId)
        {
            var sql = @"select b.* from ""Sys_UserRole"" a
left join ""Sys_Role"" b
on a.""RoleId"" = b.""Id"" and b.""zt"" = '1'
where a.""UserId"" = :userId and a.""zt"" = '1'";
            return this.FindList<SysRoleEntity>(sql, new[] { new OracleParameter(":userId", userId) });
        }

        /// <summary>
        /// 更新用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIdList"></param>
        public void UpdateUserRole(string userId, string[] roleIdList)
        {
            //角色list
            var roleLists = new List<SysUserRoleEntity>();
            foreach (var item in roleIdList.Where(p => !string.IsNullOrWhiteSpace(p)).Distinct())
            {
                var entity = new SysUserRoleEntity();
                entity.Create(true);
                entity.UserId = userId;
                entity.RoleId = item;
                entity.zt = "1";
                roleLists.Add(entity);
            }

            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var oldRoleList = db.IQueryable<SysUserRoleEntity>().Where(p => p.UserId == userId).ToList();
                for (int i = 0; i < roleLists.Count; i++)
                {
                    if (oldRoleList.Any(p => p.RoleId == roleLists[i].RoleId))
                    {
                        oldRoleList.Remove(oldRoleList.Where(p => p.RoleId == roleLists[i].RoleId).First());
                        continue;
                    }
                    db.Insert(roleLists[i]);
                }
                foreach (var item in oldRoleList)
                {
                    db.Delete(item);
                }
                db.Commit();
            }
        }

    }
}
