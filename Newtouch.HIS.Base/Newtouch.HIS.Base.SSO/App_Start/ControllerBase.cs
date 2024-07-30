using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.SSO
{
    /// <summary>
    /// Controller基类
    /// </summary>
    [HandlerLogin]
    public abstract class ControllerBase : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        protected OperatorModel UserIdentity;

        /// <summary>
        /// 
        /// </summary>
        protected string OrganizeId
        {
            get
            {
                if (this.UserIdentity == null)
                {
                    return null;
                }
                return this.UserIdentity.OrganizeId;
            }
        }

        public ControllerBase()
        {
            this.UserIdentity = OperatorProvider.GetCurrent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[HandlerAuthorize]
        public virtual ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message = null)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message }.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message, object data)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message, data = data }.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual ActionResult Error(string message)
        {
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = message }.ToJson());
        }

    }
}