using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.TherapeutistManage
{
    public class TherapeutistManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TherapeutistManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TherapeutistManage_default",
                "TherapeutistManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}