using Newtouch.Common.Operator;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Text;

namespace System.Web.Mvc
{
    /// <summary>
    /// 访问权限验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class HandlerAuthorizeAttribute : ActionFilterAttribute
    {
        static Func<IList<string>, string, string, bool> _func;

        public static void Register(Func<IList<string>, string, string, bool> func)
        {
            _func = func;
        }

        public bool Ignore { get; set; }
        public HandlerAuthorizeAttribute(bool ignore = true)
        {
            Ignore = ignore;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Ignore == false)
            {
                return;
            }
            var operatorModel = OperatorProvider.GetCurrent();
            if (operatorModel == null)
            {
                //不应该进来 //应该HandlerLoginAttribute的先执行
                WebHelper.WriteCookie("Newtouch_login_error", "overdue");
                filterContext.Result = new RedirectResult("/Login/Index");
                return;
            }
            if (operatorModel.IsAdministrator || operatorModel.IsRoot)
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
            var moduleId = WebHelper.GetCookie("Newtouch_currentmoduleid");
            var action = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString();
            return _func(operatorModel.RoleIdList, moduleId, action);
        }

    }
}
