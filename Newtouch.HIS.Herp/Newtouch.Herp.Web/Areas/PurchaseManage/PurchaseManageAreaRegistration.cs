using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.Herp.Web.Areas.PurchaseManage
{
    public class PurchaseManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "PurchaseManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "PurchaseManage_default",
                "PurchaseManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}