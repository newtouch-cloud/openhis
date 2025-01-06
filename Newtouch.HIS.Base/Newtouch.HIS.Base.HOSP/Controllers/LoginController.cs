using Newtouch.Common.Operator;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Web.Mvc;
using System.Linq;
using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Utils;
using Newtouch.Core.Redis;
using Newtouch.HIS.Web.Core.Url;
using Newtonsoft.Json.Linq;
using Newtouch.Common.Web;
using System.Diagnostics;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginController : Controller//FrameworkBase.MultiOrg.Web.Controllers.LoginController//: Controller
    {
        private readonly ISysUserApp _sysUserApp;
        private readonly ISysUserRepo _sysUserRepo;
        private readonly ISysUserLogOnRepository _sysUserLogOnRepository;
        private readonly ISysUserRoleRepo _sysUserRoleRepository;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly ISysLogRepository _sysLogRepository;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmnService;
        private readonly ISysDepartmentRepository _sysDepartmentRepository;

        /// <summary>
        /// 
        /// </summary>
        public LoginController(ISysUserApp sysUserApp, ISysUserRepo sysUserRepo, ISysUserLogOnRepository sysUserLogOnRepository
            , ISysUserRoleRepo sysUserRoleRepository, ISysUserDmnService sysUserDmnService
            , ISysLogRepository sysLogRepository, IUserRoleAuthDmnService userRoleAuthDmnService
            , ISysDepartmentRepository sysDepartmentRepository)
        {
            this._sysUserApp = sysUserApp;
            this._sysUserRepo = sysUserRepo;
            this._sysUserLogOnRepository = sysUserLogOnRepository;
            this._sysUserRoleRepository = sysUserRoleRepository;
            this._sysUserDmnService = sysUserDmnService;
            this._sysLogRepository = sysLogRepository;
            this._userRoleAuthDmnService = userRoleAuthDmnService;
            this._sysDepartmentRepository = sysDepartmentRepository;
        }

        /// <summary>
        /// 1.单点登录的回调，接收token，访问单点登录的API以获得UserId
        /// </summary>
        /// <returns></returns>
        public ActionResult SSOCallback(string access_token)
        {
            var reqObj = new
            {
                Timestamp = DateTime.Now,
                AppId = Constants.AppId,
                access_token = access_token
            };
            var apiResp = Tools.Net.HttpClientHelper.HttpPostStringAndRead<APIRequestHelper.DefaultResponse>(
                SiteUrl.GetSSOAPIUrl("api/Auth/GetOpenId"), Tools.Json.ToJson(reqObj), contentType: Newtouch.Core.Common.Utils.HttpClientHelper.EnumContentType.json);
            var openId = "";
            var jObj = Tools.Json.ToJObject(apiResp.data.ToString());
            foreach (JProperty prop in jObj.Properties())
            {
                if (prop.Name == "openId")
                {
                    openId = prop.Value.ToString();
                }
            }
            WebHelper.WriteCookie(Constants.AppId + "_OpenId", openId);
            return RedirectToAction("CheckLogin");
        }

        /// <summary>
        /// 2.验证登录
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public ActionResult CheckLogin()
        {
            var username = WebHelper.GetCookie(Constants.AppId + "_OpenId");
            WebHelper.RemoveCookie(Constants.AppId + "_OpenId");
            //登录日志Entity
            var logEntity = new SysLogEntity
            {
                Id = Guid.NewGuid().ToString(),
                TopOrganizeId = Constants.TopOrganizeId,
                Date = DateTime.Now,
                CreateTime = DateTime.Now,
                ModuleName = "系统登录",
                Type = DbLogType.Login.ToString(),
                Account = username,
            };
            SysUserEntity userEntity = null;
            if (username == "root")
            {
                userEntity = new SysUserEntity()
                {
                    Account = "root"
                };
            }
            else
            {

                userEntity = _sysUserRepo.GetEntity(Constants.TopOrganizeId, username);
            }
            if (userEntity != null)
            {
                SaveLoginSuccessStatus(userEntity, logEntity);
                _sysLogRepository.Insert(logEntity);
                //用来标志用户的登录来源
                WebHelper.WriteCookie(Constants.AppId + "_" + username + "_" + "LoginFlag", EnumLoginFlag.SSO.ToString());

                return RedirectToAction("Index", "Home");
            }
            else {
                return View();
            }
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            //写入客户端Id
            WebHelper.WriteCookie(Constants.AppId + "_ClientId", Guid.NewGuid().ToString("N").ToLower());

            ViewBag.IsCheckChkCode = ConfigurationHelper.GetAppConfigBoolValue("Is_CheckChkCode");

            return View();
        }

        [HttpGet]
        public  ActionResult GetAuthCode()
        {
            var clientId = WebHelper.GetCookie(Constants.AppId + "_ClientId");
            string code;
            var stream = new VerifyCode().GetVerifyCode(out code);

            //验证码 5分钟内有效
            RedisHelper.StringSet(clientId + "_chkCode", code, new TimeSpan(0, 5, 0));

            AppLogger.Info(string.Format("生成验证码：{0}", code));

            return File(stream, @"image/Gif");
        }

        [HttpGet]
        public ActionResult OutLogin()
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr != null)
            {
                _sysLogRepository.Insert(new SysLogEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    TopOrganizeId = Constants.TopOrganizeId,
                    Date = DateTime.Now,
                    CreateTime = DateTime.Now,
                    ModuleName = "系统登录",
                    Type = DbLogType.Exit.ToString(),
                    Account = opr.UserCode,
                    NickName = opr.UserName,
                    Result = true,
                    Description = "安全退出系统",
                });
                OperatorProvider.RemoveCurrent();
            }
            Session.Abandon();
            Session.Clear();
            var userCode = "";
            if (opr != null) { userCode = opr.UserCode; }
            var cookieLoginFlag = WebHelper.GetCookie(Constants.AppId + "_" + userCode + "_" + "LoginFlag");
            if (!string.IsNullOrWhiteSpace(cookieLoginFlag))
            {
                WebHelper.RemoveCookie(Constants.AppId + "_" + userCode + "_" + "LoginFlag");

                var url = ConfigurationHelper.GetAppConfigValue("SSOOutLogin");  //退出SSO登录

                return Redirect(url);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


        /// <summary>
        /// 返回单点登录首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ReturnSSOHome()
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr != null)
            {
                _sysLogRepository.Insert(new SysLogEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    TopOrganizeId = Constants.TopOrganizeId,
                    Date = DateTime.Now,
                    CreateTime = DateTime.Now,
                    ModuleName = "系统登录",
                    Type = DbLogType.Exit.ToString(),
                    Account = opr.UserCode,
                    NickName = opr.UserName,
                    Result = true,
                    Description = "安全退出系统",
                });
                OperatorProvider.RemoveCurrent();
            }
            Session.Abandon();
            Session.Clear();
            var userCode = "";
            if (opr != null) { userCode = opr.UserCode; }
            WebHelper.RemoveCookie(Constants.AppId + "_" + userCode + "_" + "LoginFlag");

            var url = ConfigurationHelper.GetAppConfigValue("SSOHomeHost"); ;  //跳转到单点登录页面

            return Redirect(url);
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult CheckLogin(string username, string password, string code)
        {
            //登录日志Entity
            var logEntity = new SysLogEntity
            {
                Id = Guid.NewGuid().ToString(),
                TopOrganizeId = Constants.TopOrganizeId,
                Date = DateTime.Now,
                CreateTime = DateTime.Now,
                ModuleName = "系统登录",
                Type = DbLogType.Login.ToString(),
                Account = username,
            };
            try
            {

                if (ConfigurationHelper.GetAppConfigBoolValue("Is_CheckChkCode") ?? true)
                {
                    //登录验证码 验证
                    var clientId = WebHelper.GetCookie(Constants.AppId + "_ClientId");
                    var chkCode = RedisHelper.StringGet(clientId + "_chkCode");
                    if (string.IsNullOrWhiteSpace(chkCode) || code.ToLower() != chkCode.ToLower())
                    {
                        AppLogger.Instance.Info(string.Format("验证码错误：input:{0}-db:{1}"
                            , code, chkCode));
                        throw new FailedException("验证码错误，请重新输入");
                    }
                }
                SysUserEntity userEntity = null;
                SysStaffEntity staff = null;
                if (username == "root")
                {
                    userEntity = new SysUserEntity()
                    {
                        Account = username
                    };
                }
                else
                {
                    userEntity = _sysUserApp.CheckLogin(username, password);
                }

                if (userEntity != null)
                {
                    SaveLoginSuccessStatus(userEntity, logEntity);
                }
                else
                {
                    throw new FailedException("登录失败");
                }

                _sysLogRepository.Insert(logEntity);

                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" }.ToJson());
            }
            catch (FailedException ex)
            {
                logEntity.Result = false;
                logEntity.Description = "登录失败，" + ex.Msg;
                _sysLogRepository.Insert(logEntity);
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Msg }.ToJson());
            }
            catch (StackExchange.Redis.RedisConnectionException ex)
            {
                logEntity.Result = false;
                logEntity.Description = "登录失败，" + "内存服务器无响应";
                _sysLogRepository.Insert(logEntity);
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = logEntity.Description }.ToJson());
            }
            catch (Exception ex)
            {
                AppLogger.Instance.Error(ex.Message, ex);

                logEntity.Result = false;
                logEntity.Description = "登录失败，" + ex.Message;
                _sysLogRepository.Insert(logEntity);
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "登录失败，发生内部异常" }.ToJson());
            }
        }

        /// <summary>
        /// 获取Web客户端的Ip
        /// </summary>
        public string GetWebClientIp()
        {
            string ipAddress = HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ipAddress))
            {
                return ipAddress;
            }
            ipAddress = HttpContext.Request.ServerVariables["REMOTE_ADDR"];
            if (!string.IsNullOrEmpty(ipAddress))
            {
                return ipAddress;
            }
            return string.Empty;
        }

        /// <summary>
        /// 身份验证通过后，记录成功登陆状态
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="logEntity"></param>
        private void SaveLoginSuccessStatus(SysUserEntity userEntity, SysLogEntity logEntity)
        {
            SysStaffEntity staff = null;

            var writedNeedChooseOrgFlag = false;

            OperatorModel operatorModel = null;
            if (userEntity != null)
            {
                var staffList = _sysUserDmnService.GetStaffListByUserId(userEntity.Id);
                if (userEntity.Account != "root" && userEntity.Account != "admin")
                {
                    //root admin 不可以关联医院Id，建立了关联也无视
                    staffList = _sysUserDmnService.GetStaffListByUserId(userEntity.Id);
                    if (staffList.Count > 1)
                    {
                        //主页需要做Org选择
                        WebHelper.WriteCookie(Constants.AppId + "_" + "CookieKey_ChooseOrg", "1");
                        writedNeedChooseOrgFlag = true;
                    }
                    else if (staffList.Count == 0)
                    {
                        //170904 如果尚未关联系统人员 视登录失败
                        throw new FailedException("登录失败，尚未关联系统人员");
                    }
                }

                staff = staffList != null && staffList.Count == 1 ? staffList[0] : null;
                operatorModel = new OperatorModel()
                {
                    UserId = userEntity.Id,
                    UserCode = userEntity.Account,
                    UserName = staff == null ? null : staff.Name,
                    DepartmentCode = staff == null ? null : staff.DepartmentCode,
                    OrganizeId = staff == null ? null : staff.OrganizeId,
                    LoginIPAddress = GetWebClientIp(),
                    LoginTime = DateTime.Now,
                    StaffId = staff == null ? null : staff.Id,  //人员 Id
                    rygh = staff == null ? null : staff.gh, //人员工号
                    TopOrganizeId = Constants.TopOrganizeId,
                    DepartmentName = _sysDepartmentRepository.GetNameByCode(staff == null ? null : staff.DepartmentCode
                        , staff == null ? null : staff.OrganizeId),
                };

                operatorModel.LoginIPAddressName = operatorModel.LoginIPAddress;

                if (staff != null && !string.IsNullOrWhiteSpace(staff.OrganizeId))
                {
                    WebHelper.WriteCookie(Constants.AppId + "_" + "CookieKey_OrganizeId", staff.OrganizeId);
                }
                else
                {
                    WebHelper.RemoveCookie(Constants.AppId + "_" + "CookieKey_OrganizeId");
                }

                WebHelper.WriteCookie(Constants.AppId + "_" + "CookieKey_CurUserCode", userEntity.Account);

                if (userEntity.Account == "root")
                {
                    operatorModel.IsRoot = true;
                }
                else if (userEntity.Account == "admin")
                {
                    operatorModel.IsAdministrator = true;
                }
                else
                {
                    var roleList = _userRoleAuthDmnService.GetUserRoleList(userEntity.Id,operatorModel.OrganizeId);
                    //前 已作 staff != null 的 前置判断
                    //170904 Administrator不再可以关联Staff，仅admin
                    operatorModel.RoleIdList = roleList.Select(p => p.Id).ToList();
                    operatorModel.IsHospAdministrator = roleList.Any(p => p.Code == "HospAdministrator");
                }

                WebHelper.WriteCookie(Constants.AppId + "_" + "CookieKey_IsHospAdministrator", operatorModel.IsHospAdministrator.ToString().ToLower());

                if (!writedNeedChooseOrgFlag)
                {
                    //主页需要做Org选择
                    WebHelper.RemoveCookie(Constants.AppId + "_" + "CookieKey_ChooseOrg");
                }

                logEntity.NickName = staff == null ? null : staff.Name;
                logEntity.Result = true;
                logEntity.Description = "系统登录成功";

                var rpHost = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
                if (!string.IsNullOrWhiteSpace(rpHost))
                {
                    WebHelper.WriteCookie(Constants.AppId + "_" + "ReportServer_HOST", rpHost);
                }
                else
                {
                    WebHelper.RemoveCookie(Constants.AppId + "_" + "ReportServer_HOST");
                }

                OperatorProvider.AddCurrent(operatorModel); //这个放到最后
            }
        }

        #region 统一授权登录
        /// <summary>
        /// 统一授权登录
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        //public ActionResult UALogin(string access_token, string returnUrl)
        //{
        //    if (string.IsNullOrWhiteSpace(access_token))
        //    {
        //        return RedirectToAction("Index", "Login");  //缺少令牌
        //    }
        //    var eOpr = OperatorProvider.GetCurrent();
        //    if (eOpr != null && eOpr.token == access_token)
        //    {
        //        if (!string.IsNullOrWhiteSpace(returnUrl))
        //        {
        //            return Redirect(returnUrl);
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //    }
        //    var aaa = ConfigurationHelper.GetAppConfigValue("UAUserInfoGetAPI");
        //    if (string.IsNullOrWhiteSpace(aaa))
        //    {
        //        return RedirectToAction("Index", "Login");  //未配置身份换取的API地址
        //    }
        //    //向API请求换取身份
        //    var sw = new Stopwatch();
        //    sw.Start();
        //    var reqObj = new
        //    {
        //        Timestamp = DateTime.Now,
        //        AppId = Constants.AppId,
        //        access_token = access_token
        //    };
        //    var apiResp = Tools.Net.HttpClientHelper.HttpPostStringAndRead<APIRequestHelper.DefaultResponse>(ConfigurationHelper.GetAppConfigValue("UAUserInfoGetAPI"), Tools.Json.ToJson(reqObj), contentType: Newtouch.Core.Common.Utils.HttpClientHelper.EnumContentType.json);
        //    AppLogger.Info(string.Format("{0}统一登录授权，身份获取API请求耗时{1}ms", Constants.AppId, sw.ElapsedMilliseconds));
        //    sw.Reset();
        //    if (apiResp.code != APIRequestHelper.ResponseResultCode.SUCCESS)
        //    {
        //        return RedirectToAction("Index", "Login");  //身份换取失败
        //    }
        //    sw.Start();
        //    string account = "", organizeId = "";
        //    var jObj = Tools.Json.ToJObject(apiResp.data.ToString());
        //    foreach (JProperty prop in jObj.Properties())
        //    {
        //        if (prop.Name == "Account")
        //        {
        //            account = prop.Value.ToString();
        //        }
        //        else if (prop.Name == "OrganizeId")
        //        {
        //            organizeId = prop.Value.ToString();
        //        }
        //    }

        //    Action acBeforeRedirect = () =>
        //    {
        //        AppLogger.Info(string.Format("{0}统一登录授权，QuickLoginSuccess耗时{1}ms", Constants.AppId, sw.ElapsedMilliseconds));
        //        sw.Stop();
        //    };

        //    return QuickLoginSuccess(account, organizeId, returnUrl: returnUrl, withToken: access_token, acBeforeRedirect: acBeforeRedirect);
        //}
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