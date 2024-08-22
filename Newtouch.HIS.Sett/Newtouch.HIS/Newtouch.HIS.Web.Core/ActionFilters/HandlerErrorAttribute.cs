using Newtouch.Common;
using Newtouch.Common.Exceptions;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Core.ActionFilters
{
    /// <summary>
    /// 全局异常捕获
    /// </summary>
    public class HandlerErrorAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            if (!(context.Exception is FailedException))
            {
                //记录错误日志
                var logger = AppLogger.Instance;
                if (context != null && context.Exception != null && logger != null)
                {
                    AppLogger.Instance.Error(context.Exception.Message, context.Exception);
                }
            }
            //否则为业务提示，不记日志

            if (context.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                context.ExceptionHandled = true;
                context.HttpContext.Response.StatusCode = 200;
                context.Result = new ContentResult
                {
                    Content = new AjaxResult
                    {
                        state = ResultType.error.ToString(),
                        code = (context.Exception is FailedException)
                            ? (((FailedException)context.Exception).Code ?? "").ToUpper() : "SYSTEM_ERROR",
                        message = (context.Exception is FailedException) ? ((FailedException)context.Exception).Msg : ""
                    }
                .ToJson()
                };
            }
            else
            {
                base.OnException(context);
            }
        }

    }
}
