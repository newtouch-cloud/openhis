using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;


namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 用户角色权限 公用DmnService
    /// </summary>
    public class UserRoleAuthDmnService : DmnServiceBase, IUserRoleAuthDmnService
    {
        public UserRoleAuthDmnService(IDefaultDatabaseFactory databaseFactory): base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取用户已授权的角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<SysRoleEntity> GetUserRoleList(string userId)
        {
            var sql = @"select distinct c.* from Sys_UserRole a
left join [NewtouchHIS_Base].[dbo].V_C_Sys_User b
on a.UserId = b.Id
left join Sys_Role c
on a.RoleId = c.Id
where a.UserId = @userId and a.zt = '1' and c.zt = '1'";

            return this.FindList<SysRoleEntity>(sql, new SqlParameter[] {
                new SqlParameter("@userId", userId ?? "")
            });
        }

        /// <summary>
        /// 获取UserId 根据RoleId
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<string> GetCurUserIdListByRoleId(string roleId)
        {
            var sql = "select distinct UserId from [dbo].[Sys_UserRole] where zt = '1' and RoleId = @roleId";
            return this.FindList<string>(sql, new SqlParameter[] {
                new SqlParameter("@roleId",roleId)
            });
        }

        /// <summary>
        /// 保存 角色 用户列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        public void submitRoleUser(string roleId, string userIds, string parentOrgId)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                //先 del
                var hospUserIdList = this.FindList<string>(@"
WITH cteTree
    AS (SELECT *
            FROM [NewtouchHIS_Base]..V_S_Sys_Organize
            WHERE Id = @parentOrgId --第一个查询作为递归的基点(锚点)
        UNION ALL
        SELECT [NewtouchHIS_Base]..V_S_Sys_Organize.*     --第二个查询作为递归成员， 下属成员的结果为空时，此递归结束。
            FROM
                cteTree INNER JOIN [NewtouchHIS_Base]..V_S_Sys_Organize ON cteTree.Id= [NewtouchHIS_Base]..V_S_Sys_Organize.ParentId) 


select distinct a.UserId from [NewtouchHIS_Base]..V_S_Sys_UserStaff(nolock) a
left join  [NewtouchHIS_Base]..V_S_Sys_Staff(nolock) b
on a.StaffId = b.Id
where b.OrganizeId  in (select Id from cteTree)"
, new[] { new SqlParameter("@parentOrgId", parentOrgId) });
                //
                db.Delete<SysUserRoleEntity>(p => p.RoleId == roleId && hospUserIdList.Contains(p.UserId));

                //再添加
                var userIdArr = (userIds ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
                foreach (var userId in userIdArr)
                {
                    var userRoleEntity = new SysUserRoleEntity();
                    userRoleEntity.Id = Guid.NewGuid().ToString();
                    userRoleEntity.RoleId = roleId;
                    userRoleEntity.UserId = userId;
                    userRoleEntity.Create();
                    db.Insert(userRoleEntity);
                }
                db.Commit();
            }
        }


    }
}
