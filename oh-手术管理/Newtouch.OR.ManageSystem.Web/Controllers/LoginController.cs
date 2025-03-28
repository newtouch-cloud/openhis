using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Common.Web;
using Newtouch.Core.Common.Utils;
using Newtouch.OR.ManageSystem.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// 登录登出
    /// </summary>
    public class LoginController : FrameworkBase.MultiOrg.Web.Controllers.LoginController
    {
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OutLogin()
        {
            this.WriteOutLoginLog();

            OperatorProvider.RemoveCurrent();

            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

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
                return RedirectToAction("Index", "Login"); //缺少令牌
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
                return RedirectToAction("Index", "Login"); //未配置身份换取的API地址
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
            var apiResp = Tools.Net.HttpClientHelper.HttpPostStringAndRead<APIRequestHelper.DefaultResponse>(
                ConfigurationHelper.GetAppConfigValue("UAUserInfoGetAPI"), Tools.Json.ToJson(reqObj),
                contentType: Newtouch.Core.Common.Utils.HttpClientHelper.EnumContentType.json);
            AppLogger.Info(string.Format("{0}统一登录授权，身份获取API请求耗时{1}ms", Constants.AppId, sw.ElapsedMilliseconds));
            sw.Reset();
            if (apiResp.code != APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                return RedirectToAction("Index", "Login"); //身份换取失败
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
                AppLogger.Info(string.Format("{0}统一登录授权，QuickLoginSuccess耗时{1}ms", Constants.AppId,
                    sw.ElapsedMilliseconds));
                sw.Stop();
            };

            return QuickLoginSuccess(account, organizeId, returnUrl: returnUrl, withToken: access_token,
                acBeforeRedirect: acBeforeRedirect);
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
                    addr = null; //视为null
                }
            }

            return Content(new AjaxResult {state = ResultType.success.ToString(), data = addr}.ToJson());
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
                    var appId = "Sett";
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
                                Task.Run(() => { HttpClientHelper.HttpGetString(addr1); });

                                var addr2 = addr + "?configOrganizeId=" + opr.OrganizeId;
                                Task.Run(() => { HttpClientHelper.HttpGetString(addr2); });

                                var addr3 = addr + "?outPatientRegistrationQueryPreOrganizeId=" + opr.OrganizeId;
                                Task.Run(() => { HttpClientHelper.HttpGetString(addr3); });
                            }
                        }
                    }
                }
            }
            catch
            {
            }

            #endregion
        }
    }
}