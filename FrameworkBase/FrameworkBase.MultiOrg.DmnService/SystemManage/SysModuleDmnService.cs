using System.Collections.Generic;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Domain.IRepository;
using System.Linq;
using System.Data.SqlClient;
using Newtouch.Core.Common.Utils;

namespace FrameworkBase.MultiOrg.DmnService
{
    /// <summary>
    /// 系统菜单 DmnService
    /// </summary>
    public sealed class SysModuleDmnService : DmnServiceBase, ISysModuleDmnService
    {
        private readonly ISysModuleRepo _sysModuleRepo;
        private readonly ISysRoleAuthorizeRepo _sysRoleAuthorizeRepo;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysModuleDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取系统菜单 grid
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysModuleEntity> GetMenuListByOrg(string orgId)
        {
            var sql = @"select * from Sys_Module(nolock)
where 
(
(@orgId <> '' and (OrganizeId is null or OrganizeId = @orgId))
or
(@orgId = '' and (OrganizeId is null))
)
order by isnull(px, 99999)";
            return this.FindList<SysModuleEntity>(sql, new[] { new SqlParameter("@orgId", orgId ?? "") });
        }

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysModuleEntity moduleEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                moduleEntity.Modify(keyValue);
                _sysModuleRepo.Update(moduleEntity);
            }
            else
            {
                //新增
                moduleEntity.Create(true);
                _sysModuleRepo.Insert(moduleEntity);
            }
        }

        /// <summary>
        /// 获取对当前角色开放的菜单列表（加载开放的菜单树）
        /// </summary>
        /// <param name="orgId">医疗机构</param>
        /// <param name="roleIdList"></param>
        /// <param name="isRoot"></param>
        /// <param name="isAdministrator"></param>
        /// <returns></returns>
        public IList<SysModuleEntity> GetMenuList(string orgId, IList<string> roleIdList, bool isRoot = false, bool isAdministrator = false)
        {
            var query = _sysModuleRepo.IQueryable().Where(p => p.zt == "1" && p.Target != EnumModuleTargetType.subpage.ToString());
            if (isRoot || isAdministrator)
            {
                query = query.Where(p => p.Name == "系统管理"
                    || p.Name == "系统菜单" || p.Name == "菜单管理"
                    || p.Name == "FailMsg配置"    //
                    || p.Name == "快捷菜单" //
                    || p.Name == "系统日志"
                    || p.Name == "系统用户"
                    || p.Name == "角色管理" || p.Name == "系统角色");
            }
            else
            {
                if (roleIdList == null)
                {
                    return null;
                }

                //向organize开放的
                var orgAuthedModuleId = _sysModuleRepo.IQueryable(t => t.zt == "1"
     && (t.OrganizeId == null || t.OrganizeId == orgId))
     .Select(p => p.Id).ToList();
                //向Role开放的
                var idList = _sysRoleAuthorizeRepo.IQueryable(t => t.zt == "1"
&& roleIdList.Contains(t.RoleId)).Select(p => p.ItemId);
                query = query.Where(p => orgAuthedModuleId.Contains(p.Id) && idList.Contains(p.Id));
            }
            var showDescToMenuTitle = ConfigurationHelper.GetAppConfigBoolValue("IS_ShowDescToMenuTitle");
            if (!(showDescToMenuTitle == true))
            {
                //在菜单中不显示Description
                var list = query.ToList();
                list.ForEach(p => p.Description = null);
                return list.OrderBy(t => t.px).ToList();
            }
            return query.OrderBy(t => t.px).ToList();
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="moduleId">菜单Id</param>
        public void DeleteModule(string moduleId)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                db.Delete<SysModuleEntity>(p => p.Id == moduleId);
                db.Delete<SysRoleAuthorizeEntity>(p => p.ItemId == moduleId);

                db.Commit();
            }
        }

    }
}
