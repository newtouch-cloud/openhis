using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using System.Reflection;

namespace NewtouchHIS.Framework.Web.Controllers
{
    public abstract class LoginBaseController : Controller
    {
        private readonly ILoginAppService _loginApp;
        private readonly ISysUserVDmnService _sysUserVDmn;
        public LoginBaseController(ILoginAppService loginApp, ISysUserVDmnService sysUserVDmn)
        {
            _loginApp = loginApp;
            _sysUserVDmn = sysUserVDmn;
        }
        /// <summary>
        /// override时，请最后return base.Index();
        /// </summary>
        public virtual IActionResult Index()
        {
            //写入客户端Id
            HttpWebHelper.WriteCookie(ConfigInitHelper.SysConfig.AppId + "_ClientId", Guid.NewGuid().ToString("N").ToLower());

            //是否需要验证码
            ViewBag.IsCheckChkCode = ConfigInitHelper.SysConfig.Is_CheckChkCode;

            return View();
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual IActionResult GetAuthCode()
        {
            var clientId = HttpWebHelper.GetCookie(ConfigInitHelper.SysConfig.AppId + "_ClientId");
            string code;
            CommonTools gen = new CommonTools();
            var stream = gen.GetVerifyCode(out code);

            //验证码 5分钟内有效
            RedisHelper.SetString(clientId + "_chkCode", code, new TimeSpan(0, 5, 0));

            return File(stream, @"image/Gif");
        }

        /// <summary>
        /// 验证验证码的正确性
        /// </summary>
        /// <param name="code"></param>
        /// <returns>验证码正确 返回true，否则返回false</returns>
        [NonAction]
        public virtual bool CheckChkCode(string code)
        {
            if (ConfigInitHelper.SysConfig.Is_CheckChkCode ?? true)
            {
                //登录验证码 验证
                var clientId = HttpWebHelper.GetCookie(ConfigInitHelper.SysConfig.AppId + "_ClientId");
                var chkCode = RedisHelper.GetString(clientId + "_chkCode");
                if (string.IsNullOrWhiteSpace(chkCode) || code.ToLower() != chkCode.ToLower())
                {
                    return false;
                }
            }
            return true;
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public async Task<IActionResult> CheckLogin(string username, string password, string code)
        {
            if (!CheckChkCode(code))
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "验证码错误，请重新输入" }.ToJson());
            }
            var logReq = new LoginBO { Username = username, Password = password };
            var logResult = await _loginApp.CheckPwdAsync(logReq);
            if (logResult == null || string.IsNullOrWhiteSpace(logResult.Id))
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "用户信息异常" }.ToJson());
            }
            //仅用作token换取
            OperatorModel oprInit = new OperatorModel
            {
                UserCode = logResult.Account,
                UserName = logResult.Account,
                TopOrganizeId = logResult.TopOrganizeId ?? ConfigInitHelper.SysConfig.Top_OrganizeId,
            };
            var userOpr = await _sysUserVDmn.BuildLoginStatusOpr(logResult.Id);
            if (userOpr == null || string.IsNullOrWhiteSpace(userOpr?.UserCode))
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "用户数据初始化异常" }.ToJson());
            }
            HttpWebHelper.WriteCookie(ConfigInitHelper.SysConfig.AppId + "_" + "CookieKey_CurUserCode", userOpr.UserCode);
            OprBeforeSave(userOpr);
            OperatorProvider.AddCurrent(userOpr);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功" }.ToJson());
        }

        /// <summary>
        /// 登录身份 保存之前再处理
        /// </summary>
        /// <param name="opr"></param>
        /// <returns></returns>
        public virtual void OprBeforeSave(OperatorModel opr)
        {

        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OutLogin()
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr != null)
            {
                //this.WriteOutLoginLog();

                OperatorProvider.RemoveCurrent();
            }
            //Session.Abandon();
            //Session.Clear();

            var loginFromFlag = HttpWebHelper.GetCookie(ConfigInitHelper.SysConfig.AppId + "_LoginFromFlag");
            if (!string.IsNullOrWhiteSpace(loginFromFlag))
            {
                HttpWebHelper.RemoveCookie(ConfigInitHelper.SysConfig.AppId + "_LoginFromFlag");
                ////退出SSO登录
                //var url = ConfigurationHelper.GetAppConfigValue("SSOOutLogin");
                //return Redirect(url);
            }

            return RedirectToAction("Index", "Login");
        }
        #region 统一授权登录
        /// <summary>
        /// 统一授权获取跳转地址
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public ActionResult GetUALoginAddres(string appId)
        {
            string? addr = null;
            if (!string.IsNullOrWhiteSpace(appId))
            {
                var hostName = ConfigInitHelper.SysConfig.AppAPIHostName;
                if (hostName != null)
                {

                    var app = hostName.GetType().GetPropertyList();
                    foreach (var p in app)
                    {
                        var pValue = p.GetValue(hostName);
                        if (pValue != null && pValue.ToString()?.ToLower() == appId.ToLower())
                        {
                            var host = ConfigInitHelper.SysConfig.AppAPIHost;
                            PropertyInfo propInfo = typeof(AppAPIHostConfig).GetProperty(p.Name);
                            addr = propInfo?.GetValue(host)?.ToString();
                            break;
                        }
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(addr))
            {
                addr = $"{addr}{ConfigInitHelper.SysConfig.UALoginAddress}";
                if (addr.EndsWith("/"))
                {
                    addr = addr.Substring(0, addr.Length - 1);
                }
                var token = OperatorProvider.GetCurrentToken();
                if (!string.IsNullOrWhiteSpace(token))
                {
                    if (addr.IndexOf('?') < 0)
                    {
                        addr += "?access_token=" + token;
                    }
                    else
                    {
                        addr += "&access_token=" + token;
                    }
                }
                else
                {
                    addr = null;    //视为null
                }
            }
            return Content(new AjaxResult { state = ResultType.success.ToString(), data = addr }.ToJson());
        }

        #endregion
    }
}
