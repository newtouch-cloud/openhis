using Newtouch.Common.Operator;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.SSO.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : ControllerBase
    {
        private readonly ISysApplicationRepo _sysApplicationRepo;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly LoginController _LoginController;

        public HomeController(LoginController LoginController, ISysApplicationRepo sysApplicationRepo, ISysOrganizeDmnService sysOrganizeDmnService)
        {
            this._LoginController = LoginController;
            this._sysApplicationRepo = sysApplicationRepo;
            this._sysOrganizeDmnService = sysOrganizeDmnService;
        }

        [HttpGet]
        public override ActionResult Index()
        {
            var appList = _sysOrganizeDmnService.GetAuthedAppListByTopOrgId(Constants.TopOrganizeId);

            var opr = this.UserIdentity;
            if (!opr.IsRoot && !opr.IsAdministrator)
            {
                //过滤应用
                foreach (var item in appList)
                {
                    var entity = _sysApplicationRepo.GetEntity(item.Id);
                    if (entity.Host == null)
                    {
                        break;
                    }
                    var url = entity.Host + "/Login/CheckLoginable?username=" + this.UserIdentity.UserCode;
                    if (Newtouch.Core.Common.Utils.HttpClientHelper.HttpGetString(url).ToLower() != "true")
                    {
                        item.zt = "0";
                    }
                }
            }
            appList = appList.Where(a => a.zt == "1").ToList();
            ViewBag.appList = appList;
            return View();
        }

        public ActionResult Default()
        {
            //var appList = _sysOrganizeDmnService.GetAuthedAppListByTopOrgId(Constants.TopOrganizeId);

            //var opr = this.UserIdentity;
            //if (!opr.IsRoot && !opr.IsAdministrator)
            //{
            //    //过滤应用
            //    foreach (var item in appList)
            //    {
            //        var entity = _sysApplicationRepo.GetEntity(item.Id);
            //        if (entity.Host == null)
            //        {
            //            break;
            //        }
            //        var url = entity.Host + "/Login/CheckLoginable?username=" + this.UserIdentity.UserCode;
            //        if (Newtouch.Core.Common.Utils.HttpClientHelper.HttpGetString(url).ToLower()!="true")
            //        {
            //            item.zt = "0";
            //        }
            //    }
            //}
            //appList = appList.Where(a => a.zt == "1").ToList();
            //ViewBag.appList = appList;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// 生成令牌
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public ActionResult GenerateToken(string appId)
        {
            Random rnd = new Random();
            int seed = 0;
            var rndData = new byte[4];
            rnd.NextBytes(rndData);
            var seedValue = Interlocked.Add(ref seed, 1);
            var seedData = BitConverter.GetBytes(seedValue);
            var token = Convert.ToBase64String(rndData.Concat(seedData).OrderBy(_ => rnd.Next()).ToArray()).TrimEnd('=');

            var entity = _sysApplicationRepo.GetEntity(appId);
            var url = entity.Host + "/Login/SSOLogin?tokenKey=" + token;

            var tokenObject = new
            {
                Account = this.UserIdentity.UserCode,
                UserName = this.UserIdentity.UserName,
                AppId = appId,
                Host = ConfigurationHelper.GetAppConfigValue("SSOHost")
            };
            //写进redis
            Core.Redis.RedisHelper.StringSet(token, Tools.Json.ToJson(tokenObject), new TimeSpan(0, 120, 0));

            //return Content("<script>window.open('" + url + "')</script>");
            return Content("<script>window.location.href='" + url + "'</script>");
        }

    }
}