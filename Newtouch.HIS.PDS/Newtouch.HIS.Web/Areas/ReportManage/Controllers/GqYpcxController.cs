using System;
using System.Web.Mvc;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.Tools;


namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
	public class GqYpcxController : ControllerBase
	{
		// GET: ReportManage/GqYpcx
		public ActionResult GqYpcx()
		{
			ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
			ViewBag.OrganizeId = OrganizeId;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            return View();
		}
        public ActionResult IteamSumQuery()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

    }
}