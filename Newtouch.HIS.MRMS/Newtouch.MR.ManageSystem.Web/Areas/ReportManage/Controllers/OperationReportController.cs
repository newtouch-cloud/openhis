using FrameworkBase.MultiOrg.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtouch.Core.Common.Utils;

namespace Newtouch.MR.ManageSystem.Web.Areas.ReportManage.Controllers
{
    public class OperationReportController : OrgControllerBase
	{
		// GET: ReportManage/OperationReport
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
	}
}