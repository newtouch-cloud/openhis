using Newtouch.Core.Common.Exceptions;
using Newtouch.Common.Model;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 用户角色权限 公用DmnService
    /// </summary>
    public class UserRoleAuthDmnService : DmnServiceBase, IUserRoleAuthDmnService
    {
        public UserRoleAuthDmnService(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取用户已授权的角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<SysRoleEntity> GetUserRoleList(string userId, string orgId)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(orgId))
            {
                return new List<SysRoleEntity>();
            }

            var sql = @"select distinct b.* from Sys_UserRole(nolock) a
left join Sys_Role(nolock) b
on a.RoleId = b.Id
where 1 = 1
and b.OrganizeId = @orgId
and a.UserId = @userId
and a.zt = '1' and b.zt = '1'";

            return this.FindList<SysRoleEntity>(sql, new SqlParameter[] {
                    new SqlParameter("@userId", userId)
                    ,new SqlParameter("@orgId", orgId)
                });
        }

        /// <summary>
        /// 获取角色 关联 用户（org）
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<FirstSecond> GetCurUserIdListByRoleId(string roleId)
        {
            var sql = @"select distinct UserId First, b.OrganizeId Second 
                from Sys_UserRole(nolock) a
                left join Sys_Role(nolock) b
                on a.RoleId = b.Id
                where a.zt = '1'
                and b.Id = @roleId";
            return this.FindList<FirstSecond>(sql, new SqlParameter[] {
                new SqlParameter("@roleId",roleId)
            });
        }

        /// <summary>
        /// 保存 角色 用户列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        public void submitRoleUser(string roleId, List<FirstSecond> userList)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var roleEntity = db.IQueryable<SysRoleEntity>(p => p.Id == roleId).FirstOrDefault();

                if (userList != null)
                {
                    foreach (var user in userList)
                    {
                        if (user.Second != roleEntity.OrganizeId)
                        {
                            throw new FailedException("错误的提交，用户与角色组织机构不对应");
                        }
                    }
                }

                //先 del
                db.Delete<SysUserRoleEntity>(p => p.RoleId == roleId);

                //再添加
                if (userList != null)
                {
                    foreach (var user in userList)
                    {
                        var userRoleEntity = new SysUserRoleEntity();
                        userRoleEntity.Id = Guid.NewGuid().ToString();
                        userRoleEntity.RoleId = roleId;
                        userRoleEntity.UserId = user.First;
                        userRoleEntity.Create();
                        db.Insert(userRoleEntity);
                    }
                }
                db.Commit();
            }
        }

        /// <summary>
        /// 更新用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <param name="roleIdList"></param>
        public void UpdateUserRole(string userId, string orgId, string[] roleIdList)
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
                //组织机构角色Id列表
                var orgRoleIdList = db.IQueryable<SysRoleEntity>(p => p.OrganizeId == orgId).Select(p => p.Id);
                //
                var oldRoleList = db.IQueryable<SysUserRoleEntity>().Where(p => p.UserId == userId
                    && orgRoleIdList.Contains(p.RoleId)).ToList();
                for (int i = 0; i < roleLists.Count; i++)
                {
                    //限制：关联其他组织机构的角色

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
