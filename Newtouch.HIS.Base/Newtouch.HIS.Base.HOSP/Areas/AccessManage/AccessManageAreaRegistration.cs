using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Areas.AccessManage
{
    public class AccessManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AccessManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //context.MapRoute(
            //    "AccessManage_default",
            //    "AccessManage/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);

            context.MapRoute(
                this.AreaName + "_Default",
                this.AreaName + "/{controller}/{action}/{id}",
                new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "Newtouch.HIS.Base.HOSP.Areas." + this.AreaName + ".Controllers" }
            );
        }
    }
}