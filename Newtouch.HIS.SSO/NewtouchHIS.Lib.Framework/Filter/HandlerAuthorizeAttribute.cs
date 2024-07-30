using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Operator;
using System.Text;
using ResultType = NewtouchHIS.Lib.Base.Model.ResultType;

namespace NewtouchHIS.Lib.Framework.Filter
{
    /// <summary>
    /// 过滤器 访问权限验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class HandlerAuthorizeAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 应用Id（在config配置 AppId）
        /// </summary>
        public static string _appId { get; private set; }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static HandlerAuthorizeAttribute()
        {
            _appId = ConfigInitHelper.SysConfig.AppId ?? throw new FailedException("AppId 未定义");
        }

        /// <summary>
        /// 
        /// </summary>
        private static Func<IList<string>, string, string, bool>? _func;

        /// <summary>
        /// 注册权限验证 的 方法
        /// </summary>
        /// <param name="func">角色列表、模块Id，链接（不带参数），返回是否验证通过</param>
        public static void Register(Func<IList<string>, string, string, bool> func)
        {
            _func = func;
        }


        /// <summary>
        /// （重写）Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string guid = Guid.NewGuid().ToString();
            if (filterContext.HttpContext.GetEndpoint()?.Metadata.GetMetadata<HandlerAuthorizeIgnoreAttribute>() != null)
            {
                return;
            }

            string? exception = string.Empty;
            OperatorModel? operatorModel = OperatorProvider.GetCurrent(ref exception, true);
            if (!string.IsNullOrWhiteSpace(exception))
            {
                if (exception == "SIDELINED")
                {
                    var loginErorKey = "Newtouch_login_error";
                    if (!string.IsNullOrWhiteSpace(_appId))
                    {
                        loginErorKey = _appId + "_" + loginErorKey;
                    }
                    HttpWebHelper.WriteCookie(loginErorKey, "sidelined");

                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        //is ajax 请求，则返回错误代码
                        filterContext.Result = new ContentResult
                        {
                            Content = new AjaxResult
                            {
                                state = ResultType.error.ToString(),
                                code = "SESSION_SIDELINED",
                                message = "登录超时",
                            }.ToJson()
                        };
                        return;
                    }
                    else
                    {
                        filterContext.Result = new RedirectResult("/Login/Index");
                        return;
                    }
                }
            }

            if (operatorModel == null)
            {
                var loginErorKey = "Newtouch_login_error";
                if (!string.IsNullOrWhiteSpace(_appId))
                {
                    loginErorKey = _appId + "_" + loginErorKey;
                }
                HttpWebHelper.WriteCookie(loginErorKey, "overdue");

                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    //is ajax 请求，则返回错误代码
                    filterContext.Result = new ContentResult
                    {
                        Content = new AjaxResult
                        {
                            state = ResultType.error.ToString(),
                            code = "SESSION_TIMEOUT",
                            message = "登录超时",
                        }.ToJson()
                    };
                    return;
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Login/Index");
                    return;
                }
            }
            if (operatorModel.IsAdministrator || operatorModel.IsRoot || operatorModel.IsHospAdministrator)
            {
                return;
            }
            if (!this.ActionAuthorize(filterContext, operatorModel))
            {
                StringBuilder sbScript = new StringBuilder();
                sbScript.Append("<script type='text/javascript'>alert('很抱歉！您的权限不足，访问被拒绝！');</script>");
                filterContext.Result = new ContentResult() { Content = sbScript.ToString() };
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool ActionAuthorize(ActionExecutingContext filterContext, OperatorModel operatorModel)
        {

            var curModuleIdKey = "Newtouch_currentmoduleid";
            if (!string.IsNullOrWhiteSpace(_appId))
            {
                curModuleIdKey = _appId + "_" + curModuleIdKey;
            }
            var moduleId = HttpWebHelper.GetCookie(curModuleIdKey);
            var action = filterContext.HttpContext.Request.GetDisplayUrl();  //HttpWebHelper.HttpCurrent.Request.ServerVariables["SCRIPT_NAME"].ToString();
            if(string.IsNullOrWhiteSpace(moduleId))
            {
                return true;
            }
            return _func(operatorModel.RoleIdList, moduleId, action);
        }
    }
}
