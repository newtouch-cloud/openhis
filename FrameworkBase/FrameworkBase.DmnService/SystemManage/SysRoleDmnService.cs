using System.Collections.Generic;
using System.Linq;
using FrameworkBase.Domain.IDomainServices;
using FrameworkBase.Domain.IRepository;
using FrameworkBase.Infrastructure;
using FrameworkBase.Domain.Entity;

namespace FrameworkBase.DmnService
{
    /// <summary>
    /// 角色相关
    /// </summary>
    public sealed class SysRoleDmnService : DmnServiceBase, ISysRoleDmnService
    {
        private readonly ISysModuleRepo _sysModuleRepo;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysRoleDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 提交角色 关联菜单
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="permissionIds"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysRoleEntity roleEntity, string[] permissionIds, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.Modify(keyValue);
            }
            else
            {
                roleEntity.Create(true);
            }

            var moduledata = _sysModuleRepo.IQueryable().ToList();
            List<SysRoleAuthorizeEntity> roleAuthorizeEntityList = new List<SysRoleAuthorizeEntity>();
            foreach (var itemId in permissionIds)
            {
                SysRoleAuthorizeEntity roleAuthorizeEntity = new SysRoleAuthorizeEntity();
                roleAuthorizeEntity.Create(true);
                roleAuthorizeEntity.RoleId = roleEntity.Id;
                roleAuthorizeEntity.ItemId = itemId;
                roleAuthorizeEntity.zt = "1";   //始终1 有效
                roleAuthorizeEntityList.Add(roleAuthorizeEntity);
            }

            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    List<string> igone = new List<string>();
                    db.Update(roleEntity);
                }
                else
                {
                    db.Insert(roleEntity);
                }

                db.Delete<SysRoleAuthorizeEntity>(t => t.RoleId == roleEntity.Id);

                for (int i = 0; i < roleAuthorizeEntityList.Count; i++)
                {
                    db.Insert(roleAuthorizeEntityList[i]);
                }
                db.Commit();
            }
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
                db.Delete<SysUserRoleEntity>(t => t.RoleId == keyValue);
                db.Commit();
            }
        }

    }
}
