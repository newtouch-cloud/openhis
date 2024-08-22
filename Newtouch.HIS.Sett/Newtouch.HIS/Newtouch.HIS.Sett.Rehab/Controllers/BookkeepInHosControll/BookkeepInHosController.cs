using System.Web.Mvc;

namespace Newtouch.HIS.Sett.Rehab.Controllers.BookkeepInHosControll
{
    public class BookkeepInHosController : ControllerBase
    {
        public ActionResult ChargeItemTemplate()
        {
            return View();
        }

        public ActionResult DischargeSettlement()
        {
            return View();
        }

        public ActionResult CancelSettlement()
        {
            return View();
        }
        public ActionResult HospiterResister()
        {
            return View();
        }

        /// <summary>
        /// 执行记账
        /// </summary>
        /// <returns></returns>
        public ActionResult PerformAccount()
        {
            return View();
        }
    }
}