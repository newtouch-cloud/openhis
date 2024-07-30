using System.Web.Mvc;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// 系统错误 友好体验
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// 异常发送 错误页面
        /// </summary>
        /// <returns></returns>
        public ActionResult HttpError()
        {
            return View("Error");
        }

        /// <summary>
        /// 404提醒页面
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            return Content("Not Found");
            //return View();
        }

        /// <summary>
        /// 403提醒页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Forbidden()
        {
            return Content("Forbidden");
        }
    }
}