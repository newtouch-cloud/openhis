using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common.Utils;
using Newtouch.OR.ManageSystem.Domain.DTO.InputDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.OR.ManageSystem.Web.Areas.ReportManage.Controllers
{
    public class ReportController : OrgControllerBase
    {
        // GET: ReportManage/Report
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult OperationNoticeReport(ReportReqDto req)
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrgId = OrganizeId;
            ViewBag.zyh = req.zyh;
            ViewBag.applyno = req.applyno;
            return View();
        }
    }
}