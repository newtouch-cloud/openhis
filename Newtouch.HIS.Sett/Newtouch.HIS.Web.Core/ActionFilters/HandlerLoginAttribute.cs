using Newtouch.Common.Operator;
using Newtouch.Tools;

namespace System.Web.Mvc
{
    /// <summary>
    /// 登录状态验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class HandlerLoginAttribute : AuthorizeAttribute
    {
        public bool Ignore = true;
        public HandlerLoginAttribute(bool ignore = true)
        {
            Ignore = ignore;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (Ignore == false)
            {
                return;
            }
            if (OperatorProvider.GetCurrent() == null)
            {
                WebHelper.WriteCookie("Newtouch_login_error", "overdue");
                filterContext.Result = new RedirectResult("/Login/Index");
                //filterContext.HttpContext.Response.Write("<script>top.top.location.href = '/Login/Index';</script>");
                return;
            }
        }
    }
}
