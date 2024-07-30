using FrameworkBase.MultiOrg.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
    public class yfController : OrgControllerBase
    {
        // GET: ReportManage/yf
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PharmacyMonthlyQuery()
        {
            ViewBag.OrganizeId = this.OrganizeId;
            return View();
        }
    }
}