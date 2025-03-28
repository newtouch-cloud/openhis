using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Framework.Attributes;

namespace NewtouchHIS.Lib.Framework.Filter
{
    public class FrameworkAuthorizationFilter : IAuthorizationFilter
    {
        /// <summary>
        /// 应用Id（在config配置 AppId）
        /// </summary>
        public static string _appId { get; private set; }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static FrameworkAuthorizationFilter()
        {
            _appId = ConfigInitHelper.SysConfig.AppId ?? throw new FailedException("AppId 未定义");
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //Console.WriteLine($"{nameof(FrameworkAuthorizationFilter)}:{nameof(OnAuthorization)}");
            if (context.HttpContext.GetEndpoint()?.Metadata.GetMetadata<HandlerLoginAttribute>() != null)
            {
                return;
            }
            Authorization(context);
            return;
        }
        private void Authorization(AuthorizationFilterContext filtercontext)
        {
            //登录状态验证
            HttpContext context = filtercontext.HttpContext;
            string? exception = null;
            OperatorProvider.WriteLoginUserCookie();
            var operatorModel = OperatorProvider.GetCurrent(ref exception, true);
            
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

                    if (context.Request.IsAjaxRequest())
                    {
                        //is ajax 请求，则返回错误代码
                        filtercontext.Result = new ContentResult
                        {
                            Content = new AjaxResult
                            {
                                state = ResultType.error.ToString(),
                                code = "SESSION_SIDELINED",
                                message = "登录超时",
                            }.ToJson()
                        };
                    }

                    filtercontext.Result = new RedirectResult("/Login/Index");
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

                if (context.Request.IsAjaxRequest())
                {
                    //is ajax 请求，则返回错误代码
                    filtercontext.Result = new ContentResult
                    {
                        Content = new AjaxResult
                        {
                            state = ResultType.error.ToString(),
                            code = "SESSION_TIMEOUT",
                            message = "登录超时",
                        }.ToJson()
                    };
                }
                else
                {
                    filtercontext.Result = new RedirectResult("/Login/Index");
                }
            }
            //已登录
            context.Items["IDENTITY_ORGANIZEID"] = operatorModel?.OrganizeId ?? "";
        }
    }

}
