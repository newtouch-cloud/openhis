using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Security;
using Newtouch.Core.Common.Utils;
using Newtouch.Tools;
using Newtouch.Tools.Net;
using System;
using System.Web.Mvc;
using System.Linq;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Redis;
using FrameworkBase.MultiOrg.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Web.Core.ActionFilters;
using System.Diagnostics;

namespace FrameworkBase.MultiOrg.Web.Controllers
{
    /// <summary>
    /// 登录登出
    /// </summary>
    public abstract class LoginController : AutoResolveBaseController
    {
        /// <summary>
        /// override时，请最后return base.Index();
        /// </summary>
        [HandlerDomainAutoRectify]
        public virtual ActionResult Index()
        {
            //写入客户端Id
            WebHelper.WriteCookie(ConstantsBase.AppId + "_ClientId", Guid.NewGuid().ToString("N").ToLower());

            //是否需要验证码
            ViewBag.IsCheckChkCode = ConfigurationHelper.GetAppConfigBoolValue("Is_CheckChkCode");

            return View();
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult GetAuthCode()
        {
            var clientId = WebHelper.GetCookie(ConstantsBase.AppId + "_ClientId");
            string code;
            var stream = new VerifyCode().GetVerifyCode(out code);

            //验证码 5分钟内有效
            RedisHelper.StringSet(clientId + "_chkCode", code, new TimeSpan(0, 5, 0));

            return File(stream, @"image/Gif");
        }

        /// <summary>
        /// 写登出日志
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public virtual void WriteOutLoginLog()
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr != null)
            {
                var _sysLogRepo = DependencyDefaultInstanceResolver.GetInstance<ISysLogRepo>();

                _sysLogRepo.Insert(new SysLogEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    TopOrganizeId = ConstantsBase.TopOrganizeId,
                    Date = DateTime.Now,
                    CreateTime = DateTime.Now,
                    ModuleName = "系统登录",
                    Type = DbLogType.Exit.ToString(),
                    Account = opr.UserCode,
                    NickName = opr.UserName,
                    Result = true,
                    Description = "安全退出系统",
                });
            }
        }

        /// <summary>
        /// 验证登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public virtual ActionResult CheckLogin(string username, string password, string code)
        {
            var _sysLogRepo = DependencyDefaultInstanceResolver.GetInstance<ISysLogRepo>();
            var _sysUserDmnService = DependencyDefaultInstanceResolver.GetInstance<ISysUserDmnService>();

            //登录日志Entity
            var logEntity = new SysLogEntity
            {
                Id = Guid.NewGuid().ToString(),
                TopOrganizeId = ConstantsBase.TopOrganizeId,
                Date = DateTime.Now,
                CreateTime = DateTime.Now,
                ModuleName = "系统登录",
                Type = DbLogType.Login.ToString(),
                Account = username,
            };
            try
            {
                if (!CheckChkCode(code))
                {
                    throw new FailedException("验证码错误，请重新输入");
                }
                SysUserVEntity userEntity = null;
                if (username == "root")
                {
                    if (CheckRootPwd(password))  //password是对的
                    {
                        userEntity = new SysUserVEntity()
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
                    userEntity = CheckPwd(username, password);
                }

                OperatorModel opr = null;
                if (userEntity != null)
                {
                    opr = BuildLoginStatusOpr(userEntity, logEntity);
                }
                else
                {
                    throw new FailedException("登录失败");
                }

                OprBeforeSave(opr);

                OperatorProvider.AddCurrent(opr);
                _sysLogRepo.Insert(logEntity);

                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" }.ToJson());
            }
            catch (FailedException ex)
            {
                logEntity.Result = false;
                logEntity.Description = "系统登录失败，" + ex.Msg;
                _sysLogRepo.Insert(logEntity);
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Msg }.ToJson());
            }
            catch (StackExchange.Redis.RedisConnectionException ex)
            {
                logEntity.Result = false;
                logEntity.Description = "登录失败，" + "内存服务器无响应";
                _sysLogRepo.Insert(logEntity);
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = logEntity.Description }.ToJson());
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                AppLogger.Instance.Error(ex.Message, ex);

                logEntity.Result = false;
                logEntity.Description = "系统登录失败，" + ex.Message;
                _sysLogRepo.Insert(logEntity);
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "登录失败，发生内部异常", exStackTrace = ex.Message + ex.StackTrace }.ToJson());
            }
        }

        /// <summary>
        /// 验证验证码的正确性
        /// </summary>
        /// <param name="code"></param>
        /// <returns>验证码正确 返回true，否则返回false</returns>
        [NonAction]
        public virtual bool CheckChkCode(string code)
        {
            if (ConfigurationHelper.GetAppConfigBoolValue("Is_CheckChkCode") ?? true)
            {
                //登录验证码 验证
                var clientId = WebHelper.GetCookie(ConstantsBase.AppId + "_ClientId");
                var chkCode = RedisHelper.StringGet(clientId + "_chkCode");
                if (string.IsNullOrWhiteSpace(chkCode) || code.ToLower() != chkCode.ToLower())
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// root登录（特例），验证密码
        /// 通过返回true，否则返回false
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual bool CheckRootPwd(string password)
        {
            return password == Md5.md5(DateTime.Now.ToString("HHMMdd"), 32).ToLower();
        }

        /// <summary>
        /// 验证账密
        /// 验证通过返回用户实体，否则会抛提示异常
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual SysUserVEntity CheckPwd(string username, string password)
        {
            var _sysUserDmnService = DependencyDefaultInstanceResolver.GetInstance<ISysUserDmnService>();
            return _sysUserDmnService.CheckLogin(username, password);
        }

        /// <summary>
        /// 构造登录身份（前面身份已经验证通过了）
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="logEntity"></param>
        /// <param name="staffOrganizeId">指定了OrganizeId</param>
        public virtual OperatorModel BuildLoginStatusOpr(SysUserVEntity userEntity, SysLogEntity logEntity
            , string staffOrganizeId = null)
        {
            var _sysUserDmnService = DependencyDefaultInstanceResolver.GetInstance<ISysUserDmnService>();
            var _userRoleAuthDmnService = DependencyDefaultInstanceResolver.GetInstance<IUserRoleAuthDmnService>();
            var _sysDepartmentRepo = DependencyDefaultInstanceResolver.GetInstance<ISysDepartmentRepo>();

            SysStaffVEntity staff = null;

            var writedNeedChooseOrgFlag = false;

            OperatorModel operatorModel = null;
            if (userEntity != null)
            {
                IList<SysStaffVEntity> staffList = null;
                if (userEntity.Account != "root" && userEntity.Account != "admin")
                {
                    //root admin 不可以关联医院Id，建立了关联也无视
                    staffList = _sysUserDmnService.GetStaffListByUserId(userEntity.Id);
                    if (!string.IsNullOrWhiteSpace(staffOrganizeId))
                    {
                        staffList = staffList.Where(p => p.OrganizeId == staffOrganizeId).ToList();
                    }
                    if (staffList.Count > 1)
                    {
                        //主页需要做Org选择
                        WebHelper.WriteCookie(ConstantsBase.AppId + "_" + "CookieKey_ChooseOrg", "1");
                        writedNeedChooseOrgFlag = true;
                    }
                    else if (staffList.Count == 0)
                    {
                        //170904 如果尚未关联系统人员 视登录失败
                        throw new FailedException("登录失败，尚未关联系统人员");
                    }
                }
                //staff = staffList != null && staffList.Count > 0 ? staffList[0] : null;
                staff = staffList != null && staffList.Count == 1 ? staffList[0] : null;
                operatorModel = new OperatorModel()
                {
                    UserId = userEntity.Id,
                    UserCode = userEntity.Account,
                    UserName = staff == null ? null : staff.Name,
                    DepartmentCode = staff == null ? null : staff.DepartmentCode,
                    OrganizeId = staff == null ? null : staff.OrganizeId,
                    LoginIPAddress = Newtouch.Tools.Net.NetHelper.Ip,
                    LoginTime = DateTime.Now,
                    StaffId = staff == null ? null : staff.Id,
                    rygh = staff == null ? null : staff.gh,
                    TopOrganizeId = ConstantsBase.TopOrganizeId,
                    AppId = ConstantsBase.AppId,
                    DepartmentName = staff == null ? null : _sysDepartmentRepo.GetNameByCode(staff.DepartmentCode, staff.OrganizeId),
                };

                operatorModel.LoginIPAddress = Newtouch.Tools.Net.NetHelper.Ip;
                operatorModel.LoginIPAddressName = Newtouch.Tools.Net.NetHelper.GetLocation(operatorModel.LoginIPAddress);

                if (staff != null && !string.IsNullOrWhiteSpace(staff.OrganizeId))
                {
                    WebHelper.WriteCookie(ConstantsBase.AppId + "_" + "CookieKey_OrganizeId", staff.OrganizeId);
                }
                else
                {
                    WebHelper.RemoveCookie(ConstantsBase.AppId + "_" + "CookieKey_OrganizeId");
                }

                WebHelper.WriteCookie(ConstantsBase.AppId + "_" + "CookieKey_CurUserCode", userEntity.Account);

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
                    var roleList = _userRoleAuthDmnService.GetUserRoleList(userEntity.Id, operatorModel.OrganizeId);
                    //前 已作 staff != null 的 前置判断
                    //170904 Administrator不再可以关联Staff，仅admin
                    operatorModel.RoleIdList = roleList.Select(p => p.Id).ToList();
                    operatorModel.IsHospAdministrator = roleList.Any(p => p.Code == "HospAdministrator");
                }

                if (!writedNeedChooseOrgFlag)
                {
                    //主页需要做Org选择
                    WebHelper.RemoveCookie(ConstantsBase.AppId + "_" + "CookieKey_ChooseOrg");
                }

                logEntity.NickName = staff == null ? null : staff.Name;
                logEntity.Result = true;
                logEntity.Description = "系统登录成功";
                //关联多组织机构时 不准确
                logEntity.OrganizeId = staff == null ? null : staff.OrganizeId;

                return operatorModel;
            }
            return null;
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
        /// 快捷登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="organizeId"></param>
        /// <param name="returnUrl"></param>
        /// <param name="withToken"></param>
        /// <param name="acBeforeRedirect"></param>
        /// <returns></returns>
        protected ActionResult QuickLoginSuccess(string account, string organizeId = null, string returnUrl = null, string withToken = null, Action acBeforeRedirect = null)
        {
            var sw = new Stopwatch();
            sw.Start();

            var _sysUserRepo = DependencyDefaultInstanceResolver.GetInstance<ISysUserRepo>();
            var _sysLogRepo = DependencyDefaultInstanceResolver.GetInstance<ISysLogRepo>();

            //登录日志Entity
            var logEntity = new SysLogEntity
            {
                Id = Guid.NewGuid().ToString(),
                TopOrganizeId = ConstantsBase.TopOrganizeId,
                Date = DateTime.Now,
                CreateTime = DateTime.Now,
                ModuleName = "系统登录",
                Type = DbLogType.Login.ToString(),
                Account = account,
            };
            SysUserVEntity userEntity = null;
            if (account == "root")
            {
                userEntity = new SysUserVEntity()
                {
                    Account = "root",
                };
            }
            else
            {
                userEntity = _sysUserRepo.GetEntityByUserName(account);
            }

            AppLogger.Info(string.Format("{0}QuickLoginSuccess.Inner.Part1耗时{1}ms", ConstantsBase.AppId, sw.ElapsedMilliseconds));
            sw.Reset();
            sw.Start();

            OperatorModel opr = null;
            if (userEntity != null)
            {
                opr = BuildLoginStatusOpr(userEntity, logEntity, staffOrganizeId: organizeId);
            }
            else
            {
                throw new FailedException("登录失败");
            }

            AppLogger.Info(string.Format("{0}QuickLoginSuccess.Inner.Part2耗时{1}ms", ConstantsBase.AppId, sw.ElapsedMilliseconds));
            sw.Reset();
            sw.Start();

            OprBeforeSave(opr);

            AppLogger.Info(string.Format("{0}QuickLoginSuccess.Inner.Part3耗时{1}ms", ConstantsBase.AppId, sw.ElapsedMilliseconds));
            sw.Reset();
            sw.Start();

            if (!string.IsNullOrWhiteSpace(withToken))
            {
                opr.token = withToken;
            }

            if (!(returnUrl == "null"))
            {
                OperatorProvider.AddCurrent(opr);
                _sysLogRepo.Insert(logEntity);
            }

            AppLogger.Info(string.Format("{0}QuickLoginSuccess.Inner.Part4耗时{1}ms", ConstantsBase.AppId, sw.ElapsedMilliseconds));
            sw.Stop();

            if (acBeforeRedirect != null)
            {
                acBeforeRedirect.Invoke();
            }

            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                if (returnUrl == "null")
                {
                    return null;    //no return
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// 微软SSO登录(邮箱)
        /// </summary>
        /// <param name="msEmail">密文微软邮箱</param>
        /// <param name="date">密文yyyyMMdd</param>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult MsSsoLogin(string msEmail, string date)
        {
            try
            {
                try
                {
                    string key = DateTime.Now.ToString("yyyyMMdd");
                    msEmail = DESEncrypt.Decrypt(msEmail, key);
                    long lgDate = Convert.ToInt64(DESEncrypt.Decrypt(date, key));
                    long serDate = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm"));
                    long nn = serDate - lgDate;
                    if (nn < 0 || nn > 10)//3分钟内登录有效
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = "登录已超时，请重新登录。" }.ToJson());
                    }
                    if (string.IsNullOrEmpty(msEmail))
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = "msEmail输入有误" }.ToJson());
                    }
                }
                catch (Exception ex)
                {
                    return Content(new AjaxResult { state = ResultType.error.ToString(), message = "录入的格式不正确 " + ex.Message }.ToJson());
                }

                //登录日志Entity
                var logEntity = new SysLogEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    TopOrganizeId = ConstantsBase.TopOrganizeId,
                    Date = DateTime.Now,
                    CreateTime = DateTime.Now,
                    ModuleName = "系统登录",
                    Type = DbLogType.Login.ToString(),
                };

                SysUserVEntity userEntity = null;
                var _sysUserDmnService = DependencyDefaultInstanceResolver.GetInstance<ISysUserDmnService>();
                userEntity = _sysUserDmnService.MsSsoLogin(msEmail, ConstantsBase.TopOrganizeId);
                OperatorModel opr = null;
                if (userEntity != null)
                {
                    logEntity.Account = userEntity.Account;
                    opr = BuildLoginStatusOpr(userEntity, logEntity);
                }
                else
                {
                    return Content(new AjaxResult { state = ResultType.error.ToString(), message = "msEmail未查询到匹配的用户信息" }.ToJson());
                }
                OprBeforeSave(opr);
                OperatorProvider.AddCurrent(opr);

                var _sysLogRepo = DependencyDefaultInstanceResolver.GetInstance<ISysLogRepo>();
                _sysLogRepo.Insert(logEntity);
                return RedirectToAction("../Home/Index");
                //return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" }.ToJson());
            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "异常 " + ex.ToString() }.ToJson());
                //throw new FailedException("登录失败 " + ex.Message);
            }
        }

        /// <summary>
        /// 微软SSO登录(邮箱)
        /// </summary>
        /// <param name="msEmail">密文微软邮箱</param>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult GetMsSsoUser(string msEmail)
        {
            try
            {
                try
                {
                    string key = DateTime.Now.ToString("yyyyMMdd");
                    msEmail = DESEncrypt.Decrypt(msEmail, key);
                    if (string.IsNullOrEmpty(msEmail))
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = "msEmail输入有误" }.ToJson());
                    }
                }
                catch (Exception ex)
                {
                    return Content(new AjaxResult { state = ResultType.error.ToString(), message = "录入的格式不正确 " + ex.Message }.ToJson());
                }

                SysUserVEntity userEntity = null;
                var _sysUserDmnService = DependencyDefaultInstanceResolver.GetInstance<ISysUserDmnService>();
                userEntity = _sysUserDmnService.MsSsoLogin(msEmail, ConstantsBase.TopOrganizeId);
                if (userEntity == null)
                {
                    return Content(new AjaxResult { state = ResultType.error.ToString(), message = "msEmail未查询到匹配的用户信息" }.ToJson());
                }
                var user = new { MsEmail = userEntity.MsEmail, Account = userEntity.Account };
                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。", data = user }.ToJson());
            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "异常 " + ex.ToString() }.ToJson());
            }
        }
        /// <summary>
        /// 微软SSO登录(邮箱)
        /// </summary>
        /// <param name="username">密文用户名</param>
        /// <param name="password">MD5 password</param>
        /// <param name="date">密文yyyyMMdd</param>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult MsSso(string username, string password, string date)
        {
            try
            {
                try
                {
                    string key = DateTime.Now.ToString("yyyyMMdd");
                    username = DESEncrypt.Decrypt(username, key);
                    //password = DESEncrypt.Decrypt(password, key);
                    long lgDate = Convert.ToInt64(DESEncrypt.Decrypt(date, key));
                    long serDate = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm"));
                    long nn = serDate - lgDate;
                    if (nn < 0 || nn > 10)//10分钟内登录有效
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = "登录已超时，请重新登录。" }.ToJson());
                    }
                    if (string.IsNullOrEmpty(username))
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = "username为空" }.ToJson());
                    }
                }
                catch (Exception ex)
                {
                    return Content(new AjaxResult { state = ResultType.error.ToString(), message = "录入的字符格式不正确 " + ex.Message }.ToJson());
                }

                SysUserVEntity userEntity = null;
                if (username == "root")
                {
                    if (CheckRootPwd(password))
                    {
                        userEntity = new SysUserVEntity()
                        {
                            Account = "root"
                        };
                    }
                    else
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = "密码不正确。", data = null }.ToJson());
                    }
                }
                else
                {
                    userEntity = CheckPwd(username, password);
                }

                if (userEntity == null)
                {
                    return Content(new AjaxResult { state = ResultType.error.ToString(), message = "登录失败,密码不正确。", data = null }.ToJson());
                }
                var user = new { MsEmail = userEntity.MsEmail, Account = userEntity.Account };
                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。", data = user }.ToJson());
            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "异常 " + ex.ToString() }.ToJson());
            }
        }
    }
}
