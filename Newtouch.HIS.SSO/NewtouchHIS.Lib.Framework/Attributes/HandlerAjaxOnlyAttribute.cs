using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;
using NewtouchHIS.Lib.Framework.Attributes;

namespace NewtouchHIS.Lib.Framework.Filter
{
    /// <summary>
    /// 过滤器 仅处理Ajax请求的验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class HandlerAjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HandlerAjaxOnlyAttribute()
        {

        }

        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            if (action.EndpointMetadata.Any(p => p is HandlerAjaxOnlyAttribute))
            {
                return true;
            }

            return false;
        }
    }
}
