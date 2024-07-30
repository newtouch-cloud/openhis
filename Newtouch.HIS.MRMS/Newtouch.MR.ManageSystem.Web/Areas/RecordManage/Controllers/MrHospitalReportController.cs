using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common.Utils;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.MR.ManageSystem.Web.Areas.RecordManage.Controllers
{
    public class MrHospitalReportController : OrgControllerBase
	{
        // GET: RecordManage/MrHospitalReport
   //     public ActionResult Index()
   //     {
			//return View();
   //     }

		public override ActionResult Index()
		{
			ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
			return View();
		}

		public ActionResult getOrgId()
		{
			var data = new
			{
				orgId = OrganizeId
			};
			return Content(data.ToJson());
		}
	}
}