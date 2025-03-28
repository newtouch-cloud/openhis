using FrameworkBase.MultiOrg.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
    public class PurchaseController : OrgControllerBase
    {
        // GET: ReportManage/Purchase
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PurchaseBillDetailQuery() {
            ViewBag.OrganizeId = this.OrganizeId;
            return View();
        }
    }
}