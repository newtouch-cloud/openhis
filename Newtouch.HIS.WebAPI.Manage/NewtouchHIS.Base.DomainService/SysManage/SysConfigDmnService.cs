using Mapster;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.EnumExtend;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Base.Domain.ValueObjects.SysManage;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.DataBaseSvr;

namespace NewtouchHIS.Base.DomainService.SysManage
{
    /// <summary>
    /// 系统基础配置(仅查询)
    /// </summary>
    public class SysConfigDmnService : BaseDmnService<SysOrganizeEntity>, ISysConfigDmnService
    {
        private readonly ISysRoleDmnService _roleDmn;
        /// <summary>
        /// 多应用通用功能需配置主库
        /// </summary>
        private string _mainDB = ConfigInitHelper.DbConfig.MainDB ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "DbConfig.MainDB");
        /// <summary>
        /// 应用需配置主库
        /// </summary>
        public SysConfigDmnService(ISysRoleDmnService roleDmn)
        {
            _roleDmn = roleDmn;
            base.Context = SqlSugarDbContext.Db.GetConnection(_mainDB);
        }
        /// <summary>
        /// 菜单列表（限管理员查看）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="appId"></param>
        /// <param name="userId"></param>
        /// <param name="validLimit"></param>
        /// <returns></returns>
        /// <exception cref="FailedException"></exception>
        public async Task<List<SysModuleVO>?> GetMenuListbyAppId(string orgId, string appId, string userId = null, bool validLimit = true, bool? systemMenuLimit = true)
        {
            var isAdmin = await _roleDmn.IsHospAdmin(userId, orgId);
            if (!isAdmin && userId!="root")
            {
                throw new FailedException(ResponseResultCode.FAILOfAuth, "仅限管理员操作");
            }
            string db = GetAppDB(appId) ?? "";
            if (string.IsNullOrWhiteSpace(db))
            {
                new FailedException(ResponseResultCode.FAILOfConfigInit, "AppAPIHostName");
            }
            var query = await GetByWhere<SysModuleEntity>(db, p => (p.OrganizeId == null || p.OrganizeId == orgId) && p.Target != EnumModuleTargetType.subpage.ToString());
            if (validLimit)
            {
                query = query.Where(p => p.zt == "1").ToList();
            }
            //排除系统菜单
            if (systemMenuLimit == true)
            {
                var sysMenu = query.Where(p => p.Name == "系统管理").FirstOrDefault();
                if (sysMenu != null)
                {
                    query = query.Where(p => !(p.Id == sysMenu.Id || p.ParentId == sysMenu.Id)).ToList();
                }
            }
            if (query == null || query.Count == 0)
            {
                return null;
            }
            return query.OrderBy(t => t.px).ToList().Adapt<List<SysModuleVO>>();
        }
        /// <summary>
        /// 获取对当前角色开放的菜单列表（加载开放的菜单树）
        /// 业务系统区分
        /// </summary>
        /// <param name="orgId">医疗机构</param>
        /// <param name="appId">业务系统应用Id</param>
        /// <param name="roleIdList"></param>
        /// <param name="isRoot"></param>
        /// <param name="isAdministrator"></param>
        /// <returns></returns>
        public async Task<List<SysModuleEntity>?> GetMenuList(string appId, string orgId, IList<string> roleIdList, bool isRoot = false, bool isAdministrator = false, string? userId = null, bool validLimit = true)
        {
            string db = GetAppDB(appId) ?? "";
            if (string.IsNullOrWhiteSpace(db))
            {
                throw new FailedException(ResponseResultCode.FAILOfConfigInit, "AppAPIHostName");
            }
            var query = await GetByWhere<SysModuleEntity>(db, p => (p.OrganizeId == null || p.OrganizeId == orgId) && p.Target != EnumModuleTargetType.subpage.ToString());
            if (validLimit)
            {
                query = query.Where(p => p.zt == "1").ToList();
            }
            if (query.Count() == 0)
            {
                return default;
            }
            if (isRoot || isAdministrator)
            {
                var sysMenu = query.Where(p => p.Name == "系统管理"
                    || p.Name == "系统菜单" || p.Name == "菜单管理"
                    || p.Name == "FailMsg配置"    //
                    || p.Name == "快捷菜单" //
                    || p.Name == "系统日志"
                    || p.Name == "系统用户"
                    || p.Name == "角色管理" || p.Name == "系统角色").ToList();
                query = query.Where(p => sysMenu.Select(x => x.Id).ToList().Contains(p.Id)
                    || sysMenu.Select(y => y.ParentId).Where(y => !string.IsNullOrWhiteSpace(y)).ToList().Contains(p.Id)).ToList();
            }
            else
            {
                if (roleIdList == null)
                {
                    return null;
                }

                //向organize开放的
                var orgAuthedModuleId = query.Select(p => p.Id).ToList();
                //向Role开放的                
                List<SysModuleEntity> list = new List<SysModuleEntity>();
                var idList = (await GetByWhere<SysRoleAuthorizeEntity>(db, t => t.zt == "1" && roleIdList.Contains(t.RoleId)))?.Select(p => p.ItemId);
                if (idList != null && idList.Any())
                {
                    list = query.Where(p => orgAuthedModuleId.Contains(p.Id) && idList.Contains(p.Id)).ToList();
                }
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    var dbRoleList = await _roleDmn.GetUserRoleList(userId, orgId);
                    if (dbRoleList != null && dbRoleList.Count > 0)
                    {
                        var dbIdList = (await GetByWhere<SysRoleAuthorizeEntity>(db, t => t.zt == "1" && dbRoleList.Select(p => p.Id).Contains(t.RoleId)))?.Select(p => p.ItemId);
                        if (dbIdList != null && dbIdList.Count() > 0)
                        {
                            var dbModules = query.Where(p => orgAuthedModuleId.Contains(p.Id) && dbIdList.Contains(p.Id)).ToList();
                            if (dbModules != null && dbModules.Count() > 0)
                            {
                                list.AddRange(dbModules);
                            }
                        }
                    }
                }
                if (list.Count == 0)
                {
                    return null;
                }
            }
            bool showDescToMenuTitle = AppSettings.GetAppConfigBoolValueExt("IS_ShowDescToMenuTitle");
            if (!showDescToMenuTitle)
            {
                //在菜单中不显示Description
                var list = query.ToList();
                list.ForEach(p => p.Description = null);
                return list.OrderBy(t => t.px).ToList();
            }
            return query.OrderBy(t => t.px).ToList();
        }
        /// <summary>
        /// 获取对当前角色开放的菜单列表（加载开放的菜单树）
        /// </summary>
        /// <param name="orgId">医疗机构</param>
        /// <param name="roleIdList"></param>
        /// <param name="isRoot"></param>
        /// <param name="isAdministrator"></param>
        /// <returns></returns>
        public async Task<List<SysModuleEntity>?> GetMenuList(string orgId, IList<string> roleIdList, bool isRoot = false, bool isAdministrator = false, string? userId = null, bool validLimit = true)
        {
            var query = await GetByWhere<SysModuleEntity>(p => (p.OrganizeId == null || p.OrganizeId == orgId) && p.Target != EnumModuleTargetType.subpage.ToString());
            if (validLimit)
            {
                query = query.Where(p => p.zt == "1").ToList();
            }
            if (query.Count() == 0)
            {
                return default;
            }
            if (isRoot || isAdministrator)
            {
                var sysMenu = query.Where(p => p.Name == "系统管理"
                     || p.Name == "系统菜单" || p.Name == "菜单管理"
                     || p.Name == "FailMsg配置"    //
                     || p.Name == "快捷菜单" //
                     || p.Name == "系统日志"
                     || p.Name == "系统用户"
                     || p.Name == "角色管理" || p.Name == "系统角色").ToList();
                query = query.Where(p => sysMenu.Select(x => x.Id).ToList().Contains(p.Id)
                    || sysMenu.Select(y => y.ParentId).Where(y => !string.IsNullOrWhiteSpace(y)).ToList().Contains(p.Id)).ToList();
            }
            else
            {
                if (roleIdList == null)
                {
                    return null;
                }

                //向organize开放的
                var orgAuthedModuleId = query.Select(p => p.Id).ToList();
                //向Role开放的                
                List<SysModuleEntity> list = new List<SysModuleEntity>();
                var idList = (await GetByWhere<SysRoleAuthorizeEntity>(t => t.zt == "1" && roleIdList.Contains(t.RoleId)))?.Select(p => p.ItemId);
                if (idList != null && idList.Any())
                {
                    list = query.Where(p => orgAuthedModuleId.Contains(p.Id) && idList.Contains(p.Id)).ToList();
                }
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    var dbRoleList = await _roleDmn.GetUserRoleList(userId, orgId);
                    if (dbRoleList != null && dbRoleList.Count > 0)
                    {
                        var dbIdList = (await GetByWhere<SysRoleAuthorizeEntity>(t => t.zt == "1" && dbRoleList.Select(p => p.Id).Contains(t.RoleId)))?.Select(p => p.ItemId);
                        if (dbIdList != null && dbIdList.Count() > 0)
                        {
                            var dbModules = query.Where(p => orgAuthedModuleId.Contains(p.Id) && dbIdList.Contains(p.Id)).ToList();
                            if (dbModules != null && dbModules.Count() > 0)
                            {
                                list.AddRange(dbModules);
                            }
                        }
                    }
                }
                if (list.Count == 0)
                {
                    return null;
                }
                return list;
            }
            //bool showDescToMenuTitle = AppSettings.GetAppConfigBoolValueExt("IS_ShowDescToMenuTitle");
            //if (!showDescToMenuTitle)
            //{
            //    //在菜单中不显示Description
            //    var list = query.ToList();
            //    list.ForEach(p => p.Description = null);
            //    return list.OrderBy(t => t.px).ToList();
            //}
            return query.OrderBy(t => t.px).ToList();
        }

        /// <summary>
        /// 字典表
        /// </summary>
        /// <returns></returns>
        public async Task<List<SysItemExtendVO>> GetItemDetailsList(string orgId)
        {
            List<string> org = new List<string> { "*", orgId };
            List<SysItemsDetailEntity> itemdetaildata = await GetByWhereWithAttr<SysItemsDetailEntity>(p => p.zt == "1" && org.Contains(p.OrganizeId));
            List<SysItemsEntity> itemdata = await GetByWhereWithAttr<SysItemsEntity>(p => p.zt == "1");
            return (from k in (from p in itemdata
                               where p.zt == "1"
                               select p).ToList()
                    select new
                    {
                        Type = k.Code,
                        Items = (from t in itemdetaildata
                                 where t.ItemId.Equals(k.Id)
                                 select t into p
                                 orderby p.zt == "1" descending, p.px
                                 select new { p.Name, p.Code, p.zt }).ToList()
                    }).ToList().Adapt<List<SysItemExtendVO>>();
        }



    }
}
