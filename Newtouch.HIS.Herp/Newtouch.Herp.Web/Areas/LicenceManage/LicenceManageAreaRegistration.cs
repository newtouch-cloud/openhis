using System.Web.Mvc;

namespace Newtouch.Herp.Web.Areas.LicenceManage
{
    public class LicenceManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LicenceManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LicenceManage_default",
                "LicenceManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}