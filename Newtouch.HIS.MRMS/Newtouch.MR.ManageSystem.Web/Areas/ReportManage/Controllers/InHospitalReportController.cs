using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.MR.ManageSystem.Web.Areas.ReportManage.Controllers
{
    public class InHospitalReportController : OrgControllerBase
	{
		// GET: ReportManage/InHospitalReport
		//public ActionResult Index()
		//{
		//    return View();
		//}
		public override ActionResult Index()
		{
			ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
			ViewBag.OrgId = OrganizeId;
			return View();
		}


        public  ActionResult Hospitalizationstatistics()
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrgId = OrganizeId;
            return View();
        }
        public ActionResult CyvrkyKs()
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrgId = OrganizeId;
            return View();
        }
        public ActionResult CyvrkyQy()
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrgId = OrganizeId;
            return View();
        }

        public ActionResult Typesetting()
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrgId = OrganizeId;
            return View();
        }


        public ActionResult MedicalRecord_Summary() {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrgId = OrganizeId;
            return View();
        }


    }
}