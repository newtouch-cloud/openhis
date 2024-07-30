using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NewtouchHIS.Lib.Base.Extension;
using System.Diagnostics;

namespace NewtouchHIS.Lib.Base.Filter
{
    /// <summary>
    /// 日志跟踪
    /// </summary>
    public class LogTrackingAttribute : Attribute, IActionFilter, IExceptionFilter
    {
        //private readonly ILoggerHelper _logger;
        private readonly ILogger<AppResultFilter> _logger;
        public LogTrackingAttribute(ILogger<AppResultFilter> logger)
        {
            _logger = logger;

        }
        private object _request { get; set; } = new object();

        private readonly Stopwatch logwatch = new Stopwatch();
        private bool enableLoggingMonitor = ConfigInitHelper.SysConfig?.EnableLoggingMonitorAttr ?? false;
        private bool logWithHeader = ConfigInitHelper.SysConfig?.EnableLoggingMonitorWithHeaderAttr ?? false;
        public void OnActionExecuting(ActionExecutingContext context)
        {
            logwatch.Start();//开始
            //获取请求参数
            _request = context.ActionArguments;
            if (enableLoggingMonitor && logWithHeader)
            {
                _logger.LogInformation($"[日志跟踪] OnActionExecuting ：\n[Request]\n[Request.Url]： {context.HttpContext.Request.Path.ToString()}\n[Request.Method]：{context.HttpContext.Request.Method}\n[Request.Headers]：{context.HttpContext.Request.Headers.ToDictionary(x => x.Key, v => string.Join(";", v.Value.ToList())).ToJson()}\n[Request.ExecuteStartTime]：{DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}\n[Request.Data]：{_request.ToJson()}");
            }
            else if (enableLoggingMonitor)
            {
                _logger.LogInformation($"[日志跟踪] OnActionExecuting ：\n[Request]\n[Request.Url]： {context.HttpContext.Request.Path.ToString()}\n[Request.Method]：{context.HttpContext.Request.Method}\n[Request.ExecuteStartTime]：{DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}\n[Request.Data]：{_request.ToJson()}");
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            logwatch.Stop();
            if (enableLoggingMonitor)
            {
                string result;
                var resultObj = (context.Result is ObjectResult) ? (context.Result as ObjectResult) : null;
                if (resultObj != null)
                {
                    result = resultObj.Value!.ToJson();
                }
                else
                {
                    result = context.Result!.ToJson();
                }
                _logger.LogInformation($"[日志跟踪] OnActionExecuted ：\n[Request.Url]： {context.HttpContext.Request.Path.ToString()}\n[Request.Data]：{_request.ToJson()}\n" +
                    $"[Response]\n[Response.executeEndTime]：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}\n[Response.executeTime]：{logwatch.ElapsedMilliseconds}\n[Response.Result]：{result}");
            }
        }


        public void OnException(ExceptionContext context)
        {
            _logger.LogError($"[日志跟踪] OnException ：\n[Request.Url]： {context.HttpContext.Request.Path.ToString()}\n[Request.Data]：{_request.ToJson()}\n[Response]\n[Response.executeEndTime]：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}\n[Response.executeTime]:{logwatch.ElapsedMilliseconds}\n[Response.Exception]:{context.Exception.ToJson()}");
        }

    }
}
