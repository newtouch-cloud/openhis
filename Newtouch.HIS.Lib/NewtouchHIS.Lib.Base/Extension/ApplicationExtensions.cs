using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using NewtouchHIS.Lib.Base.Model;
using System.Net;

namespace NewtouchHIS.Lib.Base.Extension
{
    public static class ApplicationExtensions
    {
        /// <summary>
        /// 抓异常
        /// </summary>
        /// <param name="app"></param>
        /// <param name="isApi"></param>
        public static void UseNewtouchExceptionHandler(this IApplicationBuilder app, bool isApi = true)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        var ex = error.Error;
                        var errorMsg = ex.Message;
                        var data = new Result
                        {
                            HttpStatus = HttpStatusCode.OK,
                            BusData = new ResponseBase
                            {
                                code = ResponseResultCode.ERROR,
                                msg = $"{errorMsg}",
#if DEBUG
                                data = ex.InnerException == null ? "" : ex.InnerException.Message
#endif
                            },

                        };
                        if (isApi)
                        {
                            await context.Response.WriteAsync(data.ToJson());
                        }
                        //else if (context.Request.Headers["x-requested-with"].ToString() == "XMLHttpRequest")
                        //{

                        //    await context.Response.WriteAsync(data.ToJson());
                        //}
                        else
                        {
                            context.Response.Redirect("/Home/Error?errorMsg=" + errorMsg + ex.InnerException);
                        }
                    }
                });
            });
        }
    }
}
