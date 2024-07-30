using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
    public class KsYpckQueryController : ControllerBase
    {
        // GET: ReportManage/KsYpckQuery
        public ActionResult KsYpckQuery()
        {
            ViewBag.OrganizeId = OrganizeId;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            return View();
        }
    }
}