using System;
using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Common.Operator;
using Newtouch.Infrastructure;
using Newtouch.Tools.Net;
using Newtouch.Common;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Core.Common.Security;

namespace Newtouch.HIS.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogApp _logApp;
        private readonly ISysUserDmnService _sysUserDmnService;

        public LoginController(ILogApp logApp, ISysUserDmnService sysUserDmnService)
        {
            this._logApp = logApp;
            this._sysUserDmnService = sysUserDmnService;
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAuthCode()
        {
            return File(new VerifyCode().GetVerifyCode("Newtouch_session_verifycode"), @"image/Gif");
        }

        [HttpGet]
        public ActionResult OutLogin()
        {
            _logApp.WriteDbLog(new LogEntity
            {
                F_ModuleName = "系统登录",
                F_Type = DbLogType.Exit.ToString(),
                F_Account = OperatorProvider.GetCurrent().UserCode,
                F_NickName = OperatorProvider.GetCurrent().UserName,
                F_Result = true,
                F_Description = "安全退出系统",
            });
            Session.Abandon();
            Session.Clear();
            OperatorProvider.RemoveCurrent();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult CheckLogin(string username, string password, string code)
        {
            LogEntity logEntity = new LogEntity();
            logEntity.F_ModuleName = "系统登录";
            logEntity.F_Type = DbLogType.Login.ToString();
            try
            {
                //#if !DEBUG
                //if (Session["Newtouch_session_verifycode"].IsEmpty() || Md5.md5(code.ToLower(), 16) != Session["Newtouch_session_verifycode"].ToString())
                //{
                //    throw new Exception("验证码错误，请重新输入");
                //}
                //#endif
                var userEntity = _sysUserDmnService.CheckLogin(username, password);
                if (userEntity != null)
                {
                    OperatorModel operatorModel = new OperatorModel();
                    operatorModel.UserId = userEntity.Id;
                    operatorModel.UserCode = userEntity.Account;
                    //operatorModel.UserName = userEntity.F_RealName;
                    //operatorModel.CompanyId = userEntity.F_OrganizeId;
                    //operatorModel.DepartmentId = userEntity.F_DepartmentId;
                    //operatorModel.RoleIdList = new List<string>() { userEntity.F_RoleId };
                    operatorModel.RoleIdList = new List<string> { null };
                    operatorModel.LoginIPAddress = NetHelper.Ip;
                    operatorModel.LoginIPAddressName = NetHelper.GetLocation(operatorModel.LoginIPAddress);
                    operatorModel.LoginTime = DateTime.Now;
                    //operatorModel.LoginToken = DESEncrypt.Encrypt(Guid.NewGuid().ToString());
                    if (userEntity.Account == "admin")
                    {
                        operatorModel.IsAdministrator = true;
                    }
                    //operatorModel.xtrybh = _sysStaffApp.GetSysRybhByAccont(userEntity.F_Account);
                    //if (operatorModel.xtrybh <= 0)
                    //{
                    //    throw new FailedException("系统人员信息不存在，请联系管理员");
                    //}
                    operatorModel.rygh = userEntity.Account;  //先默认这样
                    operatorModel.OrganizeId = "2"; //一妇婴的东院
                    OperatorProvider.AddCurrent(operatorModel);
                    logEntity.F_Account = userEntity.Account;
                    //logEntity.F_NickName = userEntity.F_RealName;
                    logEntity.F_Result = true;
                    logEntity.F_Description = "登录成功";
                    //_logApp.WriteDbLog(logEntity);
                }
                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" }.ToJson());
            }
            catch (Exception ex)
            {
                logEntity.F_Account = username;
                logEntity.F_NickName = username;
                logEntity.F_Result = false;
                logEntity.F_Description = "登录失败，" + ex.Message;
                //_logApp.WriteDbLog(logEntity);
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }

    }
}
