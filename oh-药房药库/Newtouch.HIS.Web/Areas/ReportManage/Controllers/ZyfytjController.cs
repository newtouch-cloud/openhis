using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
    public class ZyfytjController : ControllerBase
    {
        // GET: ReportManage/Zyfytj
        public ActionResult QxIndex()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }
    }
}