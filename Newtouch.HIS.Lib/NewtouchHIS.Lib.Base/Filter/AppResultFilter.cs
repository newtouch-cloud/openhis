using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using System.Diagnostics;
using System.Net;

namespace NewtouchHIS.Lib.Base.Filter
{

    /// <summary>
    /// WebApi返回结果过滤器，统一返回数据格式
    /// </summary>
    public class AppResultFilter : Attribute, IActionFilter, IResultFilter, IExceptionFilter
    {
        /// <summary>
        /// 请求时长计时开始
        /// </summary>
        private readonly Stopwatch watch = new Stopwatch();

        public AppResultFilter()
        {

        }

        #region 缓存参数 将内容保存到缓存，以便后面方便获取
        /// <summary>
        /// 请求参数
        /// </summary>
        private object? request { get; set; }

        /// <summary>
        /// 返回内容
        /// </summary>
        private Result? rets { get; set; }

        /// <summary>
        /// 请求路径
        /// </summary>
        private string? path { get; set; }
        #endregion


        /// <summary>
        /// 返回结果执行之前
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            watch.Start();//开始  
            //获取请求路径
            this.path = context.HttpContext.Request.Path;

            //获取请求头信息，如果请求头包含验证内容
            var Headers = context.HttpContext.Request.Headers;
            //var Token = Headers["Token"];

            //获取请求参数
            this.request = context.ActionArguments;
        }

        /// <summary>
        /// 返回结果执行之后
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            watch.Stop();
            //根据实际需求进行具体实现
            if (context.Result is ObjectResult)
            {
                var objectResult = context.Result as ObjectResult;
                if (objectResult == null || objectResult.Value == null)
                {
                    context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.NotFound, Message = "未找到资源", TimeOut = watch.ElapsedMilliseconds });
                }
                else
                {
                    if (objectResult.Value is Result)
                    {
                        var result = (Result)objectResult.Value;
                        result.TimeOut = watch.ElapsedMilliseconds;
                        context.Result = new JsonResult(result);
                        return;
                    }
                    context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.OK, Message = "请求成功", BusData = objectResult.Value, TimeOut = watch.ElapsedMilliseconds });
                    //判读是否返回的是元组
                    //返回数据并且返回总行数 public async Task<（List<object>,int）> Paging(PagingBaseRequest parameter) { retuen ( new List<object>{ },1000) }
                    if (objectResult.DeclaredType != null && objectResult.DeclaredType.Name == "ValueTuple`2")
                    {
                        dynamic value = objectResult.Value;
                        if (value.Item1 != null)
                        {
                            if (value.Item1 is int)
                            {
                                //返回元组格式（int,List<object>）
                                context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.OK, Message = "请求成功", TimeOut = watch.ElapsedMilliseconds, BusData = new { Count = value.Item1, Data = value.Item2 } });
                            }
                            else
                                //返回元组格式（List<object>,int）
                                context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.OK, Message = "请求成功", TimeOut = watch.ElapsedMilliseconds, BusData = new { Count = value.Item2, Data = value.Item1 } });
                        }
                    }

                }
            }
            else if (context.Result is EmptyResult)
            {
                context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.OK, Message = "请求成功" });
            }
            else if (context.Result is ContentResult)
            {
                context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.OK, Message = "", TimeOut = watch.ElapsedMilliseconds, BusData = (context.Result as ContentResult)!.Content });
            }
            else if (context.Result is StatusCodeResult)
            {
                context.Result = new JsonResult(new { HttpStatus = (context.Result as StatusCodeResult)!.StatusCode, TimeOut = watch.ElapsedMilliseconds, Message = "" });
            }
            else if (context.Result is Exception)
            {
                var result = context.Result as Exception;
                context.Result = new JsonResult(new { HttpStatus = HttpStatusCode.BadRequest, TimeOut = watch.ElapsedMilliseconds, Message = result!.Message });
            }
        }

        /// <summary>
        /// 生成结果前 这个时候已经能够拿到要返回的数据了
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is BadRequestObjectResult)
            {
                var objectResult = context.Result as ObjectResult;
                if (objectResult!.Value == null)
                {
                    context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.NotFound, Message = "未找到资源", TimeOut = watch.ElapsedMilliseconds });
                }
                else
                {
                    if (objectResult.Value is ValidationProblemDetails)
                    {
                        var result = (ValidationProblemDetails)objectResult.Value;
                        var resultmsg = string.Join(';', result.Errors.Select(p => string.Join(',', p.Value)).ToList());
                        context.Result = new JsonResult(Result.BuildResult(null, resultmsg, watch.ElapsedMilliseconds, ResponseResultCode.ERROR));
                        return;
                    }
                    else if (objectResult.Value is ProblemDetails)
                    {
                        var result = (ProblemDetails)objectResult.Value;
                        context.Result = new JsonResult(Result.BuildResult(null, result.ToJson(), watch.ElapsedMilliseconds, ResponseResultCode.ERROR));
                        return;
                    }

                    //判读是否返回的是元组
                    //返回数据并且返回总行数 public async Task<（List<object>,int）> Paging(PagingBaseRequest parameter) { retuen ( new List<object>{ },1000) }
                    if (objectResult.DeclaredType != null && objectResult.DeclaredType.Name == "ValueTuple`2")
                    {
                        dynamic value = objectResult.Value;
                        if (value.Item1 != null)
                        {
                            if (value.Item1 is int)
                            {
                                //返回元组格式（int,List<object>）
                                context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.OK, Message = "请求成功", TimeOut = watch.ElapsedMilliseconds, BusData = new { Count = value.Item1, Data = value.Item2 } });
                            }
                            else
                                //返回元组格式（List<object>,int）
                                context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.OK, Message = "请求成功", TimeOut = watch.ElapsedMilliseconds, BusData = new { Count = value.Item2, Data = value.Item1 } });
                        }
                    }

                }
            }
            else if (context.Result is EmptyResult)
            {
                context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.OK, Message = "请求成功" });
            }
            else if (context.Result is ContentResult)
            {
                context.Result = new JsonResult(new Result { HttpStatus = HttpStatusCode.OK, Message = "", TimeOut = watch.ElapsedMilliseconds, BusData = (context.Result as ContentResult)!.Content });
            }
            else if (context.Result is StatusCodeResult)
            {
                context.Result = new JsonResult(new { HttpStatus = (context.Result as StatusCodeResult)!.StatusCode, TimeOut = watch.ElapsedMilliseconds, Message = "" });
            }
            else if (context.Result is Exception)
            {
                var result = context.Result as Exception;
                context.Result = new JsonResult(new { HttpStatus = HttpStatusCode.BadRequest, TimeOut = watch.ElapsedMilliseconds, Message = result!.Message });
            }

        }


        /// <summary>
        /// 生成结果后 看需求实现
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }
        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnException(ExceptionContext context)
        {
            //是否存在未处理的错误
            if (context.ExceptionHandled == false)
            {
                //标记错误为已处理
                context.ExceptionHandled = true;
                //告诉浏览器这是正常返回
                context.HttpContext.Response.StatusCode = 200;
                context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                string errormsg = "";
                var result = new Result();
#if DEBUG
                errormsg = $"PATH:{path}======";
#endif
                errormsg = $"【异常】：{errormsg}{context.Exception.Message}";
                //获取请求头参数列表
                //var Headers_keys = "Token";
                //object hk = context.HttpContext.Request.Headers.Where(x => Headers_keys.Contains(x.Key)).ToList();

                string[] privateMsg = { Directory.GetCurrentDirectory() };
                errormsg = errormsg.Replace(privateMsg[0], string.Empty);
                //错误信息
                switch (context.Exception)
                {
                    case FailedException ex:
                        result = Result.BuildResult(ex.Data, ex.Msg, watch.ElapsedMilliseconds, ResponseResultCode.FAIL);
                        break;
                    default:
                        result = Result.BuildResult(context.Exception.InnerException, errormsg, watch.ElapsedMilliseconds, ResponseResultCode.ERROR);
                        break;
                }
                //返回错误内容
                context.Result = new JsonResult(result);
            }

        }


    }


}
