using Newtouch.Common.Operator;
using Newtouch.Core.Common.Utils;
using Newtouch.Core.Redis;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.API.Common.Models;
using Newtouch.HIS.Sett.Request.Booking.Request;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Newtouch.HIS.Sett.API.Attribute
{
    public class SimulatedUserLoginAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var o = actionContext.ActionArguments.FirstOrDefault().Value;
            var u = o as SettReq;
            var e = new RequestBase();
            string _appId = ConfigurationHelper.GetAppConfigValue("AppId");
            var orgId = ConfigurationManager.AppSettings[u.HospitalID];
            UserIdentity userIdentity = new UserIdentity()
            {
                Account = "WeChat",
                UserCode = "WeChat",
                AppId = _appId,
                UserId = "WeChat",
                UserName = "WeChat",
                OrganizeId = orgId,
                TopOrganizeId = orgId
            };
            var tk = Guid.NewGuid().ToString();
            RedisHelper.StringSet(tk, userIdentity.ToJson(), new TimeSpan(0, 20, 0));
            e.Token = tk;
            WebHelper.WriteCookie(OperatorProvider.LocalhostCookieKey, e.Token);
            HttpContext.Current.Items[(object)"API_UserIdentity_Account"] = (object)userIdentity.Account;
        }
    }
}