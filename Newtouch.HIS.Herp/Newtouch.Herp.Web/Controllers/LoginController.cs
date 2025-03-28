using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Common.Web;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Utils;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Model;
using Newtouch.Tools;

namespace Newtouch.Herp.Web.Controllers
{
    /// <summary>
    /// 登录登出
    /// </summary>
    public class LoginController : FrameworkBase.MultiOrg.Web.Controllers.LoginController
    {
        private readonly IWarehouseDmnService _warehouseDmnService;

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OutLogin()
        {
            WriteOutLoginLog();

            OperatorProvider.RemoveCurrent();

            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        /// <summary>
        /// 保存预处理
        /// </summary>
        /// <param name="opr"></param>
        public override void OprBeforeSave(OperatorModel opr)
        {
            if (string.IsNullOrWhiteSpace(opr.UserId) || string.IsNullOrWhiteSpace(opr.OrganizeId)) return;
            var kflist = _warehouseDmnService.GetKfUserInfo(opr.UserId, opr.OrganizeId);
            if (kflist != null && kflist.Count > 0)
            {
                var currentKfInfo = new LoginUserCurrentKfModel();
                foreach (var item in kflist)
                {
                    currentKfInfo.kfList.Add(new KfInfoModel
                    {
                        kfId = item.kfId,
                        kfName = item.kfName,
                        kfLeve = item.kfLeve
                    });
                }
                List<KfInfoModel> distinctkf = 
                    currentKfInfo.kfList.Where((x, i) => currentKfInfo.kfList.FindIndex(z =>z.kfId==x.kfId && z.kfName == x.kfName && z.kfLeve==x.kfLeve) == i).ToList();
                currentKfInfo.kfList= distinctkf;
                currentKfInfo.userId = opr.UserId;
                currentKfInfo.currentKfId = kflist[0].kfId;
                currentKfInfo.currentKfName = kflist[0].kfName;
                currentKfInfo.currentKfLevel = kflist[0].kfLeve;
                currentKfInfo.dutyName = kflist[0].dutyName;
                currentKfInfo.gh = kflist[0].gh;
                currentKfInfo.OrganizeId = opr.OrganizeId;
                currentKfInfo.staffName = kflist[0].staffName;
                Constants.SetCurrentKf(opr.UserId, currentKfInfo);
            }
            else
            {
                if (opr.IsAdministrator || opr.IsHospAdministrator || opr.IsRoot)
                {
                    return;
                }
                throw new FailedException("帐号配置异常，请联系管理员！");
            }
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

        #endregion
    }
}