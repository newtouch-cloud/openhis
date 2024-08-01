using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Security;
using Newtouch.Core.Common.Utils;
using Newtouch.Core.Redis;
using Newtouch.Tools;
using Newtouch.Tools.Net;
using System;
using System.Web.Mvc;
using System.Linq;
using FrameworkBase.Domain.IDomainServices;
using FrameworkBase.Domain.IRepository;
using FrameworkBase.Infrastructure;
using FrameworkBase.Domain.Entity;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Web.Core.ActionFilters;

namespace FrameworkBase.Web.Controllers
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
                SysUserEntity userEntity = null;
                if (username == "root")
                {
                    if (CheckRootPwd(password))  //password是对的
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
                logEntity.Description = "登录失败，" + ex.Msg;
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
                logEntity.Description = "登录失败，" + ex.Message;
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
        /// （特例）root登录，验证密码
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
        public virtual SysUserEntity CheckPwd(string username, string password)
        {
            var _sysUserDmnService = DependencyDefaultInstanceResolver.GetInstance<ISysUserDmnService>();
            return _sysUserDmnService.CheckLogin(username, password);
        }

        /// <summary>
        /// 构造登录身份（前面身份已经验证通过了）
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="logEntity"></param>
        public virtual OperatorModel BuildLoginStatusOpr(SysUserEntity userEntity, SysLogEntity logEntity)
        {
            var _sysDepartmentRepo = DependencyDefaultInstanceResolver.GetInstance<ISysDepartmentRepo>();
            var _userRoleAuthDmnService = DependencyDefaultInstanceResolver.GetInstance<IUserRoleAuthDmnService>();
            var _sysUserDmnService = DependencyDefaultInstanceResolver.GetInstance<ISysUserDmnService>();

            SysStaffEntity staff = null;

            if (userEntity != null)
            {
                if (userEntity.Account != "root")
                {
                    var staffList = _sysUserDmnService.GetStaffListByUserId(userEntity.Id);
                    staff = staffList != null && staffList.Count > 0 ? staffList[0] : null;
                }
                OperatorModel operatorModel = new OperatorModel()
                {
                    UserId = userEntity.Id,
                    UserCode = userEntity.Account,
                    UserName = staff == null ? null : staff.Name,
                    DepartmentCode = staff == null ? null : staff.DepartmentCode,
                    LoginIPAddress = Newtouch.Tools.Net.NetHelper.Ip,
                    LoginTime = DateTime.Now,
                    StaffId = staff == null ? null : staff.Id,  //人员 Id
                    rygh = staff == null ? null : staff.gh, //人员工号
                    DepartmentName = staff == null ? null : _sysDepartmentRepo.GetNameByCode(staff.DepartmentCode),
                };

                operatorModel.LoginIPAddress = Newtouch.Tools.Net.NetHelper.Ip;
                operatorModel.LoginIPAddressName = Newtouch.Tools.Net.NetHelper.GetLocation(operatorModel.LoginIPAddress);

                if (userEntity.Account == "root")
                {
                    operatorModel.IsRoot = true;
                }
                else
                {
                    var roleList = _userRoleAuthDmnService.GetUserRoleList(userEntity.Id);
                    operatorModel.RoleIdList = roleList.Select(p => p.Id).ToList();
                }

                logEntity.NickName = staff == null ? null : staff.Name;
                logEntity.Result = true;
                logEntity.Description = "登录成功";

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

    }
}