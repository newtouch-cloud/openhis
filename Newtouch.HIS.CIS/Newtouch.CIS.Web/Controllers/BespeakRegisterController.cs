using FrameworkBase.MultiOrg.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Controllers
{
    /// <summary>
    /// 门诊预约
    /// </summary>
    public class BespeakRegisterController : OrgControllerBase
    {
        // GET: BespeakRegister view
        public ActionResult BespeakRegister()
        {
            return View();
        }

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="yyId"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        [HttpPost]
        public ActionResult CancelYY(string yyId)
        {
            return Success();
        }
    }
}