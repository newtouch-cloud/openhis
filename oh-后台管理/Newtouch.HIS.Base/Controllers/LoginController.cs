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
using Newtouch.Core.Common.Security;
using Newtouch.Core.Common.Utils;
using Newtouch.Core.Redis;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Web.Core.Url;
using Newtonsoft.Json.Linq;
using Newtouch.Common.Web;

namespace Newtouch.HIS.Base.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginController : Controller
    {
        private readonly ISysUserApp _sysUserApp;
        private readonly ISysUserRepo _sysUserRepo;
        private readonly ISysUserLogOnRepository _sysUserLogOnRepository;
        private readonly ISysUserRoleRepo _sysUserRoleRepository;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly ISysLogRepository _sysLogRepository;

        /// <summary>
        /// 
        /// </summary>
        public LoginController(ISysUserApp sysUserApp, ISysUserRepo sysUserRepo, ISysUserLogOnRepository sysUserLogOnRepository
            , ISysUserRoleRepo sysUserRoleRepository, ISysUserDmnService sysUserDmnService
            , ISysLogRepository sysLogRepository)
        {
            this._sysUserApp = sysUserApp;
            this._sysUserRepo = sysUserRepo;
            this._sysUserLogOnRepository = sysUserLogOnRepository;
            this._sysUserRoleRepository = sysUserRoleRepository;
            this._sysUserDmnService = sysUserDmnService;
            this._sysLogRepository = sysLogRepository;
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
                    Account = username
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
        public ActionResult GetAuthCode()
        {
            var clientId = WebHelper.GetCookie(Constants.AppId + "_ClientId");
            string code;
            var stream = new VerifyCode().GetVerifyCode(out code);

            //验证码 5分钟内有效
            RedisHelper.StringSet(clientId + "_chkCode", code, new TimeSpan(0, 5, 0));

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
                        throw new FailedException("验证码错误，请重新输入");
                    }
                }
                SysUserEntity userEntity = null;
                if (username == "root")
                {
                    if (password == Md5.md5(DateTime.Now.ToString("HHMMdd"), 32).ToLower())  //password是对的
                    {
                        userEntity = new SysUserEntity()
                        {
                            Account = "root"
                        };
                    }
                    else
                    {
                        throw new FailedException("密码不正确，请重新输入");
                    }
                }
                else
                {
                    throw new FailedException("登录名不正确");
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
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Msg }.ToJson());
            }
            catch (StackExchange.Redis.RedisConnectionException ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "登录失败，" + "内存服务器无响应" }.ToJson());
            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }


        /// <summary>
        /// 身份验证通过后，记录成功登陆状态
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="logEntity"></param>
        private void SaveLoginSuccessStatus(SysUserEntity userEntity, SysLogEntity logEntity)
        {
            if (userEntity != null)
            {
                OperatorModel operatorModel = new OperatorModel()
                {
                    UserId = userEntity.Id,
                    UserCode = userEntity.Account,
                };

                if (userEntity.Account == "root")
                {
                    operatorModel.IsRoot = true;
                }
                else
                {

                }
                OperatorProvider.AddCurrent(operatorModel);
            }
        }
    }
}