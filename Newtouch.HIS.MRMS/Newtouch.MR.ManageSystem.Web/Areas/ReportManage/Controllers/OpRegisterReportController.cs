using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common.Utils;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.MR.ManageSystem.Web.Areas.ReportManage.Controllers
{
    public class OpRegisterReportController : OrgControllerBase
	{
		// GET: ReportManage/OpRegister
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