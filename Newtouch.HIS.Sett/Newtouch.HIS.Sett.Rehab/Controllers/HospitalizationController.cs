using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Sett.Rehab.Controllers
{
    /// <summary>
    /// 住院相关
    /// </summary>
    public class HospitalizationController : ControllerBase
    {
        // GET: Hospitalization
        public ActionResult Index()
        {
            return View();
        }
    }
}