using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.OutpatientManage.Controllers
{
    public class QueryForSettleController : ControllerBase
    {
        // GET: OutpatientManage/QueryForJs
        public ActionResult QueryForJs()
        {
            return View();
        }

        public ActionResult DenyReg()
        {
            return View();
        }
    }
}