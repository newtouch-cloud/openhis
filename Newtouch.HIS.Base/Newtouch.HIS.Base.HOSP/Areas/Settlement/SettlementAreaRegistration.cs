﻿using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Areas.Controllers.Settlement
{
    public class PSettlementAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Settlement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                this.AreaName + "_Default",
                this.AreaName + "/{controller}/{action}/{id}",
                new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "Newtouch.HIS.Base.HOSP.Areas." + this.AreaName + ".Controllers" }
            );
        }
    }
}
