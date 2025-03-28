using System;
using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.Common.Operator;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices;
using System.Linq;
using Newtouch.Infrastructure.Model;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Web.Core.Url;
using Newtouch.Common.Web;
using Newtonsoft.Json.Linq;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginController : FrameworkBase.MultiOrg.Web.Controllers.LoginController
    {
        private readonly ISysUserExDmnService _sysUserDmnService;
        private readonly ISysPharmacyDepartmentRepo _sysPharmacyDepartmentRepo;
        private readonly ISysLogRepo _sysLogRepo;
        private readonly ISysUserRepo _sysUserRepo;

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
        /// 统一授权预热（预热打开CIS页面）
        /// </summary>
        /// <param name="opr"></param>
        public static void UALoginPre(OperatorModel opr)
        {
            try
            {
                if (opr != null && !string.IsNullOrWhiteSpace(opr.OrganizeId))
                {
                    var appId = opr.AppId;
                    if (!string.IsNullOrWhiteSpace(appId))
                    {
                        var addrList = new List<string>();
                        //
                        var addrKey = appId + "_UALoginPreAddress";
                        var iaddr = ConfigurationHelper.GetAppConfigValue(addrKey);
                        if (!string.IsNullOrWhiteSpace(iaddr))
                        {
                            addrList.Add(iaddr);
                        }
                        //
                        addrKey = appId + "_UALoginPreAddress2";
                        iaddr = ConfigurationHelper.GetAppConfigValue(addrKey);
                        if (!string.IsNullOrWhiteSpace(iaddr))
                        {
                            addrList.Add(iaddr);
                        }

                        foreach (var addr in addrList)
                        {
                            if (!string.IsNullOrWhiteSpace(addr))
                            {
                                var addr1 = addr + "?account=" + opr.UserCode + "&organizeId=" + opr.OrganizeId;
                                Task.Run(() =>
                                {
                                    HttpClientHelper.HttpGetString(addr1);
                                });

                                var addr2 = addr + "?configOrganizeId=" + opr.OrganizeId;
                                Task.Run(() =>
                                {
                                    HttpClientHelper.HttpGetString(addr2);
                                });

                                var addr3 = addr + "?outPatientRegistrationQueryPreOrganizeId=" + opr.OrganizeId;
                                Task.Run(() =>
                                {
                                    HttpClientHelper.HttpGetString(addr3);
                                });
                            }
                        }
                    }
                }
            }
            catch
            {
            }
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
        /// 退出登录
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opr"></param>
        public override void OprBeforeSave(OperatorModel opr)
        {
            //更新用户药房部分
            if (ConfigurationHelper.GetAppConfigBoolValue("Is_UserYfbmRelated") == true && opr.OrganizeId != null)
            {
                //当前要操作的药房部门
                if (!string.IsNullOrWhiteSpace(opr.UserId) && !string.IsNullOrWhiteSpace(opr.OrganizeId))
                {
                    opr.yfbmCodeList = _sysUserDmnService.GetYfbmCodeListByUserId(opr.UserId, opr.OrganizeId);
                }
                if (opr.yfbmCodeList != null && opr.yfbmCodeList.Count > 0)
                {
                    //当前要操作的药房部门    //在OrgId关联了多个yfbm怎么办
                    var curYfbmCode = opr.yfbmCodeList.FirstOrDefault();
                    Constants.SetCurrentYfbm(opr.UserId, new LoginUserCurrentYfbmModel()
                    {
                        yfbmCode = curYfbmCode,
                        yfbmjb = _sysPharmacyDepartmentRepo.GetYjbmjbByCode(curYfbmCode, opr.OrganizeId),
                        mzzybz = _sysPharmacyDepartmentRepo.GetMzzybzByCode(curYfbmCode, opr.OrganizeId),
                    });
                }
                else
                {
                    Constants.SetCurrentYfbm(opr.UserId, null);  //移除
                }
            }

            //
            WebHelper.WriteCookie(Constants.AppId + "_" + "CookieKey_IsHospAdministrator", opr.IsHospAdministrator.ToString().ToLower());

            //
            var rpHost = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            if (!string.IsNullOrWhiteSpace(rpHost))
            {
                WebHelper.WriteCookie(Constants.AppId + "_" + "ReportServer_HOST", rpHost);
            }
            else
            {
                WebHelper.RemoveCookie(Constants.AppId + "_" + "ReportServer_HOST");
            }

            UALoginPre(opr);
        }

    }
}
