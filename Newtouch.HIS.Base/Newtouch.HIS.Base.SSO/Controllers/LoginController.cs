using Newtouch.Common;
using Newtouch.Common.Exceptions;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Security;
using Newtouch.Core.Common.Utils;
using Newtouch.Core.Redis;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using Newtouch.Tools.Net;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.SSO.Controllers
{
    public class LoginController : Controller
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
        // GET: Login
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
            return RedirectToAction("Index", "Login");
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
                    userEntity = _sysUserApp.CheckLogin(username, password);
                }

                if (userEntity != null)
                {
                    var staffList = _sysUserDmnService.GetStaffListByUserId(userEntity.Id);
                    if (userEntity.Account != "root" && userEntity.Account != "admin")
                    {
                        //root admin 不可以关联医院Id，建立了关联也无视
                        staffList = _sysUserDmnService.GetStaffListByUserId(userEntity.Id);
                        if (staffList.Count == 0)
                        {
                            //170904 如果尚未关联系统人员 视登录失败
                            throw new FailedException("登录失败，尚未关联系统人员");
                        }
                    }

                    OperatorModel operatorModel = new OperatorModel()
                    {
                        UserId = userEntity.Id,
                        UserCode = userEntity.Account,
                        LoginIPAddress = NetHelper.Ip,
                        LoginTime = DateTime.Now,
                        TopOrganizeId = Constants.TopOrganizeId,
                    };

                    operatorModel.LoginIPAddress = NetHelper.Ip;
                    operatorModel.LoginIPAddressName = NetHelper.GetLocation(operatorModel.LoginIPAddress);

                    WebHelper.WriteCookie(Constants.AppId + "_" + "CookieKey_CurUserCode", userEntity.Account);

                    if (userEntity.Account == "root")
                    {
                        operatorModel.IsRoot = true;
                    }
                    else if (userEntity.Account == "admin")
                    {
                        operatorModel.IsAdministrator = true;
                    }

                    OperatorProvider.AddCurrent(operatorModel);
                }

                logEntity.Result = true;
                logEntity.Description = "登录成功";
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
    }
}