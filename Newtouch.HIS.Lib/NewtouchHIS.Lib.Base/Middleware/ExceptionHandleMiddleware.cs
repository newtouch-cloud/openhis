using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Extensions.Logging;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Lib.Base.Middleware
{
    /// <summary>
    /// 异常处理中间件
    /// </summary>
    public class ExceptionHandleMiddleware
    {
        private readonly ILogger<ExceptionHandleMiddleware> _logger;
        private readonly RequestDelegate next;
        private IHostingEnvironment environment;

        public ExceptionHandleMiddleware(RequestDelegate next, IHostingEnvironment environment, ILogger<ExceptionHandleMiddleware> logger)
        {
            _logger = logger;
            this.next = next;
            this.environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
                var features = context.Features;
            }
            catch (Exception e)
            {
                await HandleException(context, e);
            }
        }

        private async Task HandleException(HttpContext context, Exception e)
        {
            context.Response.StatusCode = 200;
            context.Response.ContentType = "text/json;charset=utf-8;";
            string error = "";
            string logError = $"PATH:{context.Request.Path}\nRequest:{context.Request.Query.ToJson()}";
            _logger.LogError(logError);
            if (environment.IsDevelopment())
            {
                //错误信息
                switch (e)
                {
                    case FailedException ex:
                        error = new AjaxResult { state = ResultType.error.ToString(), message = ex.Msg, exStackTrace = ex.Data.ToJson() }.ToJson();
                        if (ex.Log)
                        {
                            _logger.LogError(error);
                        }
                        break;
                    case RuntimeBinderException ex:
                        error = new AjaxResult { state = ResultType.error.ToString(), message = e.Message, exStackTrace = e.StackTrace, InnerException = e.InnerException }.ToJson();
                        break;
                    default:
                        error = new AjaxResult { state = ResultType.error.ToString(), message = e.Message, exStackTrace = e.StackTrace, InnerException = e.InnerException }.ToJson();
                        _logger.LogError(error);
                        break;
                }
            }
            else
            {
                //错误信息
                switch (e)
                {
                    case FailedException ex:
                        error = new AjaxResult { state = ResultType.error.ToString(), message = ex.Msg }.ToJson();
                        if (ex.Log)
                        {
                            _logger.LogError(error);
                        }
                        break;
                    default:
                        error = new AjaxResult { state = ResultType.error.ToString(), message = "抱歉，系统处理异常！请查看系统日志。" }.ToJson();
                        _logger.LogError(new AjaxResult { state = ResultType.error.ToString(), message = e.Message, exStackTrace = e.StackTrace, InnerException = e.InnerException }.ToJson());
                        break;
                }
            }
            _logger.LogError(error);
            await context.Response.WriteAsync(error);
        }
    }
}
