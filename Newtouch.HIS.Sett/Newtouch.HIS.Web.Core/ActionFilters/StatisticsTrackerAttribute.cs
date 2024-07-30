using Newtouch.Common.Exceptions;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;
using Newtouch.Common.Attributes;
using Newtouch.Common.Operator;
using Newtouch.Common;

namespace Newtouch.HIS.Web.Core.ActionFilters
{
    /// <summary>
    /// 系统访问统计跟踪日志
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class StatisticsTrackerAttribute : ActionFilterAttributeBase
    {
        private readonly string Key = "_thisOnActionMonitorLog_";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionDescriptor.GetCustomAttributes(false).Any(p => p is TrackerIgnoreAttribute))
            {
                return;
            }

            filterContext.Controller.ViewData[Key] = DateTime.Now;

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.ActionDescriptor.GetCustomAttributes(false).Any(p => p is TrackerIgnoreAttribute))
            {
                return;
            }

            var enterTime = filterContext.Controller.ViewData[Key] as DateTime?;

            if (enterTime.HasValue)
            {
                //记录访问日志
                var areaName = filterContext.RouteData.Values["area"] as string;
                var controllerName = filterContext.RouteData.Values["controller"] as string;
                var actionName = filterContext.RouteData.Values["action"] as string;
                var costMSec = (DateTime.Now - enterTime.Value).Milliseconds;
                var ip = "";
                var isAjax = filterContext.RequestContext.HttpContext.Request.IsAjaxRequest();
                //响应状态码
                int responseStatusCode = 200;
                string responseSubCode = null;
                string responseSubMsg = null;
                if (filterContext.Exception != null)
                {
                    if (filterContext.Exception is FailedException)
                    {
                        //为业务提示
                        responseStatusCode = -200;
                        responseSubCode = ((FailedException)filterContext.Exception).Code;
                        if (!string.IsNullOrWhiteSpace(responseSubCode))
                        {
                            responseSubCode = responseSubCode.ToUpper();
                        }
                        responseSubMsg = ((FailedException)filterContext.Exception).Msg;
                    }
                    else
                    {
                        responseStatusCode = -500;  //有异常
                    }
                }

                NameValueCollection requestQueryString = null, requestForm = null;
                RouteValueDictionary routeDataCollections = null;
                requestQueryString = filterContext.HttpContext.Request.QueryString; //Url 参数
                routeDataCollections = filterContext.RouteData.Values;
                if (!filterContext.ActionDescriptor.GetCustomAttributes(false).Any(p => p is TrackerRequestFormIgnoreAttribute))
                {
                    requestForm = filterContext.HttpContext.Request.Form;   //form表单提交的数据
                }

                //登录用户标示
                var op = OperatorProvider.GetCurrent();
                string userIdentity = op != null ? op.UserCode : null;

                var str = Json.ToJson(new
                {
                    Id = Guid.NewGuid().ToString(),
                    AreaName = areaName,
                    ControllerName = controllerName,
                    ActionName = actionName,
                    EnterTime = enterTime,
                    CostMSec = costMSec,
                    IP = ip,
                    IsAjax = isAjax,
                    RequestQueryString = toDictionary(requestQueryString), //Url 参数
                    RequestForm = toDictionary(requestForm),   //form表单提交的数据
                    RouteDataCollections = toDictionary(routeDataCollections),
                    ResponseStatusCode = responseStatusCode,
                    ResponseSubCode = responseSubCode,
                    ResponseSubMsg = responseSubMsg,
                    UserIdentity = userIdentity,
                });

                AppLogger.Instance.Moniter(string.Format(@"
-statisticstrackerlog-S----------------##---------------------
{0}
-statisticstrackerlog-E----------------##---------------------
                    ", str));

                if (str.Length > 1000)
                {
                    AppLogger.Instance.Warn("存在比较大的Monitor记录，请核查");
                }
            }

            base.OnActionExecuted(filterContext);
        }


        #region private methods

        /// <summary>
        /// 获取路由参数
        /// </summary>
        /// <param name="rDict"></param>
        /// <returns></returns>
        private Dictionary<string, object> toDictionary(RouteValueDictionary rDict)
        {
            Dictionary<string, object> dict = null;
            if (rDict != null)
            {
                foreach (var i in rDict)
                {
                    if (i.Key != "controller" && i.Key != "action" && i.Key != "area" && i.Value != null
                        && (i.Value.GetType() == typeof(string) || i.Value.GetType() == typeof(int) || i.Value.GetType() == typeof(bool))
                    )
                    {
                        dict = dict ?? new Dictionary<string, object>();
                        dict.Add(i.Key, i.Value.ToString());
                    }
                }
            }
            return dict;
        }

        /// <summary>
        /// 获取Post 或Get 参数
        /// </summary>
        /// <param name="collections"></param>
        /// <returns></returns>
        private Dictionary<string, object> toDictionary(NameValueCollection collections)
        {
            Dictionary<string, object> dict = null;
            if (collections != null && collections.Count > 0)
            {
                dict = new Dictionary<string, object>();
                foreach (string key in collections.Keys)
                {
                    dict.Add(key, collections[key]);
                }
            }
            return dict;
        }

        #endregion

    }

}
