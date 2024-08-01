using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Linq;
using FrameworkBase.Domain.IRepository;
using FrameworkBase.Domain.Entity;
using FrameworkBase.Infrastructure;

namespace FrameworkBase.Repository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:19
    /// 描 述：用户角色表
    /// </summary>
    public sealed class SysUserRoleRepo : RepositoryBase<SysUserRoleEntity>, ISysUserRoleRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public SysUserRoleRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取Role关联UserId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<string> GetUserIdListByRoleId(string roleId)
        {
            var sql = "select UserId from Sys_UserRole(nolock) where RoleId = @roleId and zt = '1'";
            return this.FindList<string>(sql, new SqlParameter[] {
                new SqlParameter("@roleId",roleId)
            });
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

        /// <summary>
        /// 提交角色用户关联关系
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        public void SubmitRoleUser(string roleId, string userIds)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var userIdArr = (userIds ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct();

                db.Delete<SysUserRoleEntity>(p => p.RoleId == roleId);

                //再添加
                foreach (var user in userIdArr)
                {
                    var userRoleEntity = new SysUserRoleEntity();
                    userRoleEntity.Id = Guid.NewGuid().ToString();
                    userRoleEntity.RoleId = roleId;
                    userRoleEntity.UserId = user;
                    userRoleEntity.Create();
                    db.Insert(userRoleEntity);
                }
                db.Commit();
            }
        }

    }
}