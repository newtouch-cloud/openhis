﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.DeanInquiryManage
{
    public class DeanInquiryManageAreaRegistration: AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "DeanInquiryManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                this.AreaName + "_Default",
                this.AreaName + "/{controller}/{action}/{id}",
                new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "Newtouch.HIS.Web.Areas." + this.AreaName + ".Controllers" }
            );
        }
    }
}