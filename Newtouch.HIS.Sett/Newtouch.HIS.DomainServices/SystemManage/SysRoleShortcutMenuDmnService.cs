using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.DmnService;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 快捷菜单
    /// </summary>
    public class SysRoleShortcutMenuDmnService : DmnServiceBase, ISysRoleShortcutMenuDmnService
    {
        public SysRoleShortcutMenuDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取角色开放的快捷菜单 List
        /// </summary>
        /// <param name="roleIdList"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysShortcutMenuEntity> GetAuthedSCMList(IList<string> roleIdList, string orgId)
        {
            var sql = @"select * from (select distinct a.* from Sys_ShortcutMenu a
left join Sys_RoleShortcutMenu b
on a.Id = b.SCMId
where a.zt = '1' and b.zt = '1' and b.OrganizeId = @orgId
and b.RoleId in (select * from dbo.f_split(@jsnmStr, ','))
) as sc
order by isnull(px, 99999), CreateTime desc";
            return this.FindList<SysShortcutMenuEntity>(sql, new[] { new SqlParameter("@orgId", orgId),
                new SqlParameter("@jsnmStr", string.Join(",", roleIdList))});
        }

        /// <summary>
        /// 获取快捷菜单 已授权的角色列表
        /// </summary>
        /// <param name="scmId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<string> GetAuthedRoleIdList(string scmId, string orgId)
        {
            var sql = @"select distinct b.RoleId from Sys_ShortcutMenu a
left join Sys_RoleShortcutMenu b
on a.Id = b.SCMId
where a.zt = '1' and b.zt = '1' and b.OrganizeId = @orgId and a.Id = @scmId";
            return this.FindList<string>(sql, new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@scmId", scmId) });
        }

        /// <summary>
        /// 快捷菜单 授权 给指定 角色
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="roleList"></param>
        public void UpdateAuthRoleList(string scmId, string roleList, string orgId)
        {
            var roleIdArr = (roleList ?? "").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                db.Delete<SysRoleShortcutMenuEntity>(p => p.SCMId == scmId && p.OrganizeId == orgId);

                //再添加
                foreach (var roleId in roleIdArr)
                {
                    var entity = new SysRoleShortcutMenuEntity();
                    entity.Id = Guid.NewGuid().ToString();
                    entity.RoleId = roleId;
                    entity.SCMId = scmId;
                    entity.OrganizeId = orgId;
                    entity.Create(true);
                    db.Insert(entity);
                }
                db.Commit();
            }
        }

    }
}
