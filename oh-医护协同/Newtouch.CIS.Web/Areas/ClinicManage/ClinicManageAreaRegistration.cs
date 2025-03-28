using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.ClinicManage
{
    public class ClinicManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ClinicManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ClinicManage_default",
                "ClinicManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}