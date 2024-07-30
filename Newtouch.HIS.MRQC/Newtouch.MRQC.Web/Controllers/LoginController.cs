using System;
using System.Diagnostics;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtonsoft.Json.Linq;
using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Common.Web;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Web.Core.Url;
using Newtouch.MRQC.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.MRQC.Web.Controllers
{
    /// <summary>
    /// 登录登出
    /// </summary>
    public class LoginController : FrameworkBase.MultiOrg.Web.Controllers.LoginController
    {
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly ISysLogRepo _sysLogRepo;
        private readonly ISysUserRepo _sysUserRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        public override void OprBeforeSave(OperatorModel opr)
        {
            
        }

        #region 单点登录start

        /// <summary>
        /// 1.单点登录的回调，接收token，访问单点登录的API以获得UserId
        /// </summary>
        /// <returns></returns>
        public ActionResult SSOCallback(string access_token, string returnUrl)
        {
            var reqObj = new
            {
                Timestamp = DateTime.Now,
                AppId = Constants.AppId,
                access_token = access_token
            };
            var apiResp = Tools.Net.HttpClientHelper.HttpPostStringAndRead<APIRequestHelper.DefaultResponse>(
                SiteUrl.GetSSOAPIUrl("api/Auth/GetOpenId"), Tools.Json.ToJson(reqObj), contentType: Newtouch.Core.Common.Utils.HttpClientHelper.EnumContentType.json);
            var openId = "";    //这是Account
            var jObj = Tools.Json.ToJObject(apiResp.data.ToString());
            foreach (JProperty prop in jObj.Properties())
            {
                if (prop.Name == "openId")
                {
                    openId = prop.Value.ToString();
                }
            }

            Action acBeforeRedirect = () =>
            {
                //用来标志用户的登录来源
                WebHelper.WriteCookie(Constants.AppId + "_LoginFromFlag", EnumLoginFlag.SSO.ToString());
            };
            return QuickLoginSuccess(openId, returnUrl: returnUrl, acBeforeRedirect: acBeforeRedirect);
        }

        /// <summary>
        /// 返回单点登录首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ReturnSSOHome()
        {
            //相当于先做退出操作
            var opr = OperatorProvider.GetCurrent();
            if (opr != null)
            {
                this.WriteOutLoginLog();

                OperatorProvider.RemoveCurrent();
            }
            Session.Abandon();
            Session.Clear();

            WebHelper.RemoveCookie(Constants.AppId + "_LoginFromFlag");

            //跳转回单点登录页面
            var url = ConfigurationHelper.GetAppConfigValue("SSOHomeHost");

            return Redirect(url);
        }

        #endregion 单点登录 end
        #region 统一授权登录

        /// <summary>
        /// 统一授权登录
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult UALogin(string access_token, string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(access_token))
            {
                return RedirectToAction("Index", "Login");  //缺少令牌
            }
            var eOpr = OperatorProvider.GetCurrent();
            if (eOpr != null && eOpr.token == access_token)
            {
                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            var aaa = ConfigurationHelper.GetAppConfigValue("UAUserInfoGetAPI");
            if (string.IsNullOrWhiteSpace(aaa))
            {
                return RedirectToAction("Index", "Login");  //未配置身份换取的API地址
            }
            //向API请求换取身份
            var sw = new Stopwatch();
            sw.Start();
            var reqObj = new
            {
                Timestamp = DateTime.Now,
                AppId = Constants.AppId,
                access_token = access_token
            };
            var apiResp = Tools.Net.HttpClientHelper.HttpPostStringAndRead<APIRequestHelper.DefaultResponse>(ConfigurationHelper.GetAppConfigValue("UAUserInfoGetAPI"), Tools.Json.ToJson(reqObj), contentType: Newtouch.Core.Common.Utils.HttpClientHelper.EnumContentType.json);
            AppLogger.Info(string.Format("{0}统一登录授权，身份获取API请求耗时{1}ms", Constants.AppId, sw.ElapsedMilliseconds));
            sw.Reset();
            if (apiResp.code != APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                return RedirectToAction("Index", "Login");  //身份换取失败
            }
            sw.Start();
            string account = "", organizeId = "";
            var jObj = Tools.Json.ToJObject(apiResp.data.ToString());
            foreach (JProperty prop in jObj.Properties())
            {
                if (prop.Name == "Account")
                {
                    account = prop.Value.ToString();
                }
                else if (prop.Name == "OrganizeId")
                {
                    organizeId = prop.Value.ToString();
                }
            }

            Action acBeforeRedirect = () =>
            {
                AppLogger.Info(string.Format("{0}统一登录授权，QuickLoginSuccess耗时{1}ms", Constants.AppId, sw.ElapsedMilliseconds));
                sw.Stop();
            };

            return QuickLoginSuccess(account, organizeId, returnUrl: returnUrl, withToken: access_token, acBeforeRedirect: acBeforeRedirect);
        }

        /// <summary>
        /// 统一授权获取跳转地址
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public ActionResult GetUALoginAddres(string appId)
        {
            string addr = null;
            if (!string.IsNullOrWhiteSpace(appId))
            {
                appId = appId + "_UALoginAddress";
                addr = ConfigurationHelper.GetAppConfigValue(appId);
            }
            if (!string.IsNullOrWhiteSpace(addr))
            {
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

        /// <summary>
        /// 统一授权登录预热
        /// </summary>
        /// <param name="account"></param>
        /// <param name="organizeId"></param>
        /// <param name="configOrganizeId">预热动态参数配置</param>
        /// <param name="outPatientRegistrationQueryPreOrganizeId">门诊患者查询API预热</param>
        /// <returns></returns>
        public ActionResult UALoginPre(string account, string organizeId, string configOrganizeId
            , string outPatientRegistrationQueryPreOrganizeId)
        {
            if (!string.IsNullOrWhiteSpace(configOrganizeId))
            {
                //系统动态参数配置
                _sysConfigRepo.GetByCode("any_any_any_any", configOrganizeId);
            }
            if (!string.IsNullOrWhiteSpace(account) && !string.IsNullOrWhiteSpace(organizeId))
            {
                //模拟登录
                QuickLoginSuccess(account, organizeId, returnUrl: "null");
            }
            if (!string.IsNullOrWhiteSpace(outPatientRegistrationQueryPreOrganizeId))
            {
                //门诊患者查询
                var url = ConfigurationHelper.GetAppConfigValue("UAOutPatientRegistrationQueryPreAPI");
                if (!string.IsNullOrWhiteSpace(url))
                {
                    url += "?organizeId=" + outPatientRegistrationQueryPreOrganizeId;
                    var apiResp =
    Newtouch.Core.Common.Utils.HttpClientHelper.HttpGetString(url);
                }
            }
            return null;
        }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public override ActionResult Index()
        {
            //移除SSO登录来源标记
            WebHelper.RemoveCookie(Constants.AppId + "_LoginFromFlag");

            return base.Index();
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OutLogin()
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr != null)
            {
                this.WriteOutLoginLog();

                OperatorProvider.RemoveCurrent();
            }
            Session.Abandon();
            Session.Clear();

            var loginFromFlag = WebHelper.GetCookie(Constants.AppId + "_LoginFromFlag");
            if (!string.IsNullOrWhiteSpace(loginFromFlag))
            {
                WebHelper.RemoveCookie(Constants.AppId + "_LoginFromFlag");
                //退出SSO登录
                var url = ConfigurationHelper.GetAppConfigValue("SSOOutLogin");
                return Redirect(url);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

    }
}