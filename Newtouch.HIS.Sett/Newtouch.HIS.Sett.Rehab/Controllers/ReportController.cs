using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Sett.Rehab.Controllers
{
    /// <summary>
    /// 报表
    /// </summary>
    public class ReportController : ControllerBase
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
    }
}