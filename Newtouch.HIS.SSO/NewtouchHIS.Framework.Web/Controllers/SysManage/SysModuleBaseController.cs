using Mapster;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Framework.Web.ServiceModels;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Filter;

namespace NewtouchHIS.Framework.Web.Controllers.SysManage
{
    public class SysModuleBaseController : OrgControllerBase
    {
        protected readonly ISysModuleDmnService _sysModuleDmnService;
        protected readonly ISysConfigDmnService _sysConfigDmn;

        public SysModuleBaseController(ISysModuleDmnService sysModuleDmnService, ISysConfigDmnService sysConfigDmn)
        {
            _sysModuleDmnService = sysModuleDmnService;
            _sysConfigDmn = sysConfigDmn;
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetBsTreeSelectJson(bool justTop = true, bool withSystemMenu = true)
        {
            var tree = new List<BsTreeSelectModel>();
            var data = await _sysConfigDmn.GetMenuListbyAppId(OrganizeId, ConfigInitHelper.SysConfig.AppId, UserIdentity.UserId, true, withSystemMenu);

            if (data == null || data.Count == 0)
            {
                return Content(tree.ToJson());
            }
            if (justTop)
            {
                tree = data.Where(p => string.IsNullOrWhiteSpace(p.ParentId)).Select(m => new BsTreeSelectModel
                {
                    href = m.Id,
                    text = m.Name,
                }).ToList();
            }
            else
            {
                tree = data.Select(m => new BsTreeSelectModel
                {
                    href = m.Id,
                    text = m.Name,
                }).ToList();
            }
            return Content(tree.ToJson());
        }

        /// <summary>
        /// 下拉（选择菜单 编辑 上级）
        /// </summary>
        /// <param name="justTop">仅顶级</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetTreeSelectJson(bool justTop = true, bool withSystemMenu = true)
        {
            var tree = new List<TreeSelectModel>();
            var data = await _sysConfigDmn.GetMenuListbyAppId(OrganizeId, ConfigInitHelper.SysConfig.AppId, UserIdentity.UserId, true, withSystemMenu);

            if (data == null || data.Count == 0)
            {
                return Content(tree.ToJson());
            }
            if (justTop)
            {
                tree = data.Where(p => string.IsNullOrWhiteSpace(p.ParentId)).Select(m => new TreeSelectModel
                {
                    id = m.Id,
                    text = m.Name,
                    parentId = m.ParentId
                }).ToList();
            }
            else
            {
                tree = data.Select(m => new TreeSelectModel
                {
                    id = m.Id,
                    text = m.Name,
                    parentId = m.ParentId
                }).ToList();
            }

            return Content(tree.ToJson());
        }

        /// <summary>
        /// 业务系统菜单列表 grid json
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public async virtual Task<IActionResult> GetAppMenuTreeGridJson(OLPagination<MenuAuthRequest> request)
        {
            if (request.queryParams == null || string.IsNullOrWhiteSpace(request.queryParams.MenuAppId))
            {
                return Content("");
            }
            var appMenuData = await _sysConfigDmn.GetMenuListbyAppId(OrganizeId, request.queryParams.MenuAppId, UserIdentity.UserId, request.queryParams.ValidLimit);
            if (appMenuData == null || appMenuData.Count == 0)
            {
                return Content("");
            }
            var thisSysMenu = await _sysConfigDmn.GetMenuList(OrganizeId, UserIdentity.RoleIdList, UserIdentity.IsRoot, UserIdentity.IsAdministrator, UserIdentity.UserId, request.queryParams.ValidLimit);
            var appMenuList = appMenuData.GroupJoin(thisSysMenu, a => new { a.Name, a.UrlAddress }, b => new { b.Name, b.UrlAddress }, (a, c) => new SysModuleExtendVO
            {
                Id = a.Id,
                Name = a.Name,
                Icon = a.Icon,
                zt = a.zt,
                UrlAddress = a.UrlAddress,
                px = a.px,
                Description = a.Description,
                ParentId = a.ParentId,
                OrganizeId = a.OrganizeId,
                IsSync = c.Select(ac => ac.Id).Count() > 0 ? true : false,
                CustomOrder = string.IsNullOrEmpty(a.ParentId) ? a.Id.Replace("-", "") + "0" : a.ParentId.Replace("-", "") + "1" + a.px
            }).ToList();
            if (!(request.queryParams.ShowSync ?? false))
            {
                appMenuList = appMenuList.Where(p => p.IsSync == false).ToList();
            }

            return Content(appMenuList.ToJson());
        }
        /// <summary>
        /// 本系统菜单列表 grid json
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public async virtual Task<IActionResult> GetTreeGridJson(OLPagination<QueryParamsRequest> request)
        {
            if (UserIdentity.IsRoot)
            {
                var rootMenu = await _sysConfigDmn.GetMenuListbyAppId(OrganizeId, ConfigInitHelper.SysConfig.AppId, UserIdentity.UserId, false, false);
                return Content(rootMenu?.ToJson());
            }
            var data = await _sysConfigDmn.GetMenuList(OrganizeId, UserIdentity.RoleIdList, UserIdentity.IsRoot, UserIdentity.IsAdministrator, UserIdentity.UserId, request.queryParams.validLimit ?? false);
            if (data == null || data.Count == 0)
            {
                return Content("");
            }
            if (request.queryParams != null && !string.IsNullOrWhiteSpace(request.queryParams.keyword))
            {
                data = data.Where(p => p.Name.Contains(request.queryParams.keyword)).ToList();
            }
            return Content(data.ToJson());
        }

        /// <summary>
        /// GetFormJson
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public async virtual Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _sysModuleDmnService.GetEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 提交保存菜单
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public async Task<IActionResult> SubmitForm(MenuAddRequest moduleEntity, string? keyValue = null)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                if (!this.UserIdentity.IsRoot && !this.UserIdentity.IsAdministrator)
                {
                    if (string.IsNullOrEmpty(this.OrganizeId))
                    {
                        return Error("操作失败。");
                    }
                    moduleEntity.OrganizeId = this.OrganizeId;
                }
            }
            else
            {
                //是修改行为
                if (this.OrganizeId != ConfigInitHelper.SysConfig.Top_OrganizeId
                    && this.OrganizeId != moduleEntity.OrganizeId)
                {
                    {
                        return Error("操作失败。数据错误");
                    }
                }
                moduleEntity.Id = moduleEntity.Id ?? keyValue;
            }

            moduleEntity.zt = moduleEntity.zt == "true" ? "1" : "0";
            var request = moduleEntity.Adapt<SysModuleVO>();
            var response = string.IsNullOrEmpty(keyValue) ?
                await _sysModuleDmnService.AddEntity(request, this.UserIdentity.UserCode, OrganizeId)
                : await _sysModuleDmnService.UpdateEntity(request, this.UserIdentity.UserCode, OrganizeId);
            if (response != null && response.code == ResponseResultCode.SUCCESS)
            {
                return Success("操作成功");
            }
            return Error($"{response?.msg}({moduleEntity.Name})");
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                return Error("菜单信息异常，请刷新重试");
            }
            var response = await _sysModuleDmnService.DelEntity(keyValue, this.UserIdentity.UserCode, OrganizeId);
            if (response != null && response.code == ResponseResultCode.SUCCESS)
            {
                return Success("操作成功");
            }
            return Error($"{response?.msg}");
        }
    }
}
