using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;

namespace NewtouchHIS.Framework.Web.Controllers
{
    /// <summary>
    /// 已登录 Controller基类
    /// </summary>
    public abstract class BaseController : Controller
    {

        private OperatorModel? _userIdentity;
        /// <summary>
        /// 登录用户身份
        /// </summary>
        protected internal OperatorModel? UserIdentity
        {
            get
            {
                if (_userIdentity == null)
                {
                    _userIdentity = OperatorProvider.GetCurrent();
                }
                return _userIdentity;
            }
        }

        /// <summary>
        /// 列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public virtual IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 表单页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual IActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 返回Success
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual IActionResult Success(string? message = null)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message }.ToJson());
        }

        /// <summary>
        /// 返回Success
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected virtual IActionResult Success(string? message, object? data)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message, data = data }.ToJson());
        }

        /// <summary>
        /// 返回Error
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual IActionResult Error(string? message)
        {
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = message }.ToJson());
        }
    }
}
