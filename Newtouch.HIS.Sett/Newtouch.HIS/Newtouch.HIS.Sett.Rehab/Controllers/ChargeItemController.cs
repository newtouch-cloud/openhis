using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Sett.Rehab.Controllers
{
    /// <summary>
    /// 收费项目
    /// </summary>
    public class ChargeItemController : ControllerBase
    {
        public ActionResult ShowChargeItem()
        {
            return View();
        }
    }
}