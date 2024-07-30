using Newtouch.Common.Operator;
using Newtouch.HIS.Domain.IDomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
    public class JsYpcxController : ControllerBase
    {
        private readonly IfyDmnService _ifyDmnService;

        // GET: ReportManage/JsYpcx
        public ActionResult QxIndex()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }
        public ActionResult YPCKMXIndex()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        public ActionResult GetDlList()
        {
            var data = _ifyDmnService.getYpdl(OrganizeId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}