using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.OR.ManageSystem.Web.Areas.Operation.Controllers
{
    public class OpFeeReportController : OrgControllerBase
	{
        // GET: Operation/OpFeeReport
  //      public ActionResult Index()
  //      {
  //          return View();
		//}
		public override ActionResult Index()
		{
			ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
			ViewBag.OrgId = OrganizeId;
			return View();
		}

	}
}