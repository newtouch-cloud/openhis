using HIS.SSO.Domain.IDomainServices;
using HIS.SSO.Domain.Model.SysManage;
using HIS.SSO.Models;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Framework.Web.ServiceModels;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Filter;
using StackExchange.Redis;
using System.Diagnostics;
using System.Reflection;

namespace HIS.SSO.Controllers
{
    public class HomeController : OrgControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShortcutMenuDmnService _shortcutmenudmnservice;
        private readonly ISysUserStaffDutyDmnService _sysuserstaffdutydmnservice;

        public HomeController(ILogger<HomeController> logger, IShortcutMenuDmnService shortcutmenudmnservice, ISysUserStaffDutyDmnService sysuserstaffdutydmnservice)
        {
            _logger = logger;
            _shortcutmenudmnservice = shortcutmenudmnservice;
            _sysuserstaffdutydmnservice = sysuserstaffdutydmnservice;

        }

        public IActionResult DashBoard()
        {
            return View();
        }
        public override IActionResult Index()
        {
            var loginFromFlag = HttpWebHelper.GetCookie(ConfigInitHelper.SysConfig.AppId + "_LoginFromFlag");
            ViewBag.loginFromFlag = loginFromFlag ?? "UNKNOWN";
            return View();
        }

        public IActionResult WorkSpace()
        {
            return View();
        }
        public IActionResult SSOIndex()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region  工作台

        /// <summary>
        /// 快捷菜单list
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public async virtual Task<IActionResult> GetShortcutMenuList(string keyWord)
        {
            var data = await _shortcutmenudmnservice.GetShortcutMenuList(this.UserIdentity.UserCode, this.OrganizeId);
            //if (data == null || data.Count == 0)
            //{
            //    return Content("");
            //}
            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                data = data.Where(p => p.MenuName.Contains(keyWord)).ToList();
            }
            return Content(data.ToJson());
        }

        /// <summary>
		/// 快捷菜单收藏
		/// </summary>
		/// <param name="keyword"></param>
		/// <returns></returns>
		[HttpPost]
        [HandlerAjaxOnly]
        public async virtual Task<IActionResult> SaveShortcutMenu(ShorcutMenuModel request)
        {
            if (string.IsNullOrWhiteSpace(request.returnUrl))
            {
                return Error("操作失败。数据错误");
            }
            request.OrganizeId = this.OrganizeId;
            request.CreatorCode = this.UserIdentity.UserCode;
            request.MenuUrl = request.returnUrl;
            if (!string.IsNullOrWhiteSpace(request.access_token))
            {
                var hostHost = ConfigInitHelper.SysConfig.AppAPIHost;
                if (hostHost != null)
                {
                    var app = hostHost.GetType().GetPropertyList();
                    foreach (var p in app)
                    {
                        var pValue = p.GetValue(hostHost);
                        if (pValue != null && pValue.ToString() == request.host)
                        {
                            var host = ConfigInitHelper.SysConfig.AppAPIHostName;
                            PropertyInfo propInfo = typeof(AppAPIHostConfig).GetProperty(p.Name);
                            request.AppId = propInfo?.GetValue(host)?.ToString();
                            break;
                        }
                    }
                }
            }
            var data = await _shortcutmenudmnservice.SaveShortcutMenu(request);
            if (data != null && data.code == ResponseResultCode.SUCCESS)
            {
                return Success("操作成功");
            }
            return Error($"{data?.msg}");
        }

        /// <summary>
        /// 登陆角色岗位
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public async virtual Task<IActionResult> GetUserDutyList()
        {
            var UserdutyList = await _sysuserstaffdutydmnservice.GetStaffDutyListByOrganizeId(this.OrganizeId, this.UserIdentity.StaffId);
            if (UserdutyList.Count > 0)
            {
                var dutyList = UserdutyList.Where(p => p.StaffId != null && (p.DutyCode == "tollman" || p.DutyCode == "Doctor" || p.DutyCode == "Nurse")).Select(p => new { id = p.DutyCode, text = p.DutyName }).ToList();
                return Content(dutyList.ToJson());
            }
            return Content("");

        }
        /// <summary>
        /// 表格统计
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public async virtual Task<IActionResult> GetDutyTotalList(string dutycode, DateTime tjrq)
        {
            var data = await _shortcutmenudmnservice.GetDutyTotalList(dutycode, tjrq, this.UserIdentity.UserCode, this.OrganizeId,UserIdentity.IsHospAdministrator);
            if(dutycode== "tollman" || dutycode=="Doctor")
            {
                data.Add(new Domain.ValueObjects.SysManage.HomeDataTotalVo
                {
                    brxz = "合计",
                    ghrc = data.Sum(p => p.ghrc),
                    thrc = data.Sum(p => p.thrc),
                    jsje = data.Sum(p => p.jsje),
                    tfje = data.Sum(p => p.tfje)
                });
            }else if(dutycode== "Nurse")
            {
                data.Add(new Domain.ValueObjects.SysManage.HomeDataTotalVo
                {
                    brxz = "合计",
                    yrqrc = data.Sum(p => p.yrqrc),
                    wrqrc = data.Sum(p => p.wrqrc),
                    zcw = data.Select(p => p.zcw).FirstOrDefault(),
                    sycw = data.Select(p => p.sycw).FirstOrDefault(),
                    jrcqrc= data.Sum(p => p.jrcqrc),
                    jrwcqrc= data.Sum(p => p.jrwcqrc),
                });
            }
            return Content(data.ToJson());
        }

        #endregion
    }
}