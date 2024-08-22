using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.FeeConfirmManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class FeeConfirmController : Controller
    {
        // GET: FeeConfirmManage/FeeConfirm
        /// <summary>
        /// 收费确认
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 处方退费
        /// </summary>
        /// <returns></returns>
        public ActionResult Refund()
        {
            return View();
        }

        /// <summary>
        /// 处方列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PrescriptionForm()
        {
            return View();
        }
    }
}