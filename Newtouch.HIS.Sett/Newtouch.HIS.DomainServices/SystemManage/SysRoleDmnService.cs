using Newtouch.HIS.Domain.IDomainServices;
using System.Collections.Generic;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using System.Linq;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 角色 DmnService
    /// </summary>
    public class SysRoleDmnService : DmnServiceBase, ISysRoleDmnService
    {
        public SysRoleDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 删除系统角色
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                db.Delete<SysRoleEntity>(t => t.Id == keyValue);
                db.Delete<SysRoleAuthorizeEntity>(t => t.RoleId == keyValue);
                db.Commit();
            }
        }

        /// <summary>
        /// 提交新建、更新 实体
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="roleAuthorizeEntitys"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysRoleEntity roleEntity, List<SysRoleAuthorizeEntity> roleAuthorizeEntitys, string keyValue)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var dbRoleEntity = db.IQueryable<SysRoleEntity>(p => p.Id == keyValue).FirstOrDefault();

                    dbRoleEntity.Name = roleEntity.Name;
                    dbRoleEntity.Code = roleEntity.Code;
                    dbRoleEntity.px = roleEntity.px;
                    dbRoleEntity.zt = roleEntity.zt;
                    dbRoleEntity.Description = roleEntity.Description;

                    dbRoleEntity.Modify();
                    db.Update(dbRoleEntity);
                }
                else
                {
                    //roleEntity.Create(true);
                    db.Insert(roleEntity);
                }

                db.Delete<SysRoleAuthorizeEntity>(t => t.RoleId == roleEntity.Id);

                for (int i = 0; i < roleAuthorizeEntitys.Count; i++)
                {
                    db.Insert(roleAuthorizeEntitys[i]);
                }
                db.Commit();
            }
        }

    }
}
