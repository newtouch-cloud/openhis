using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.NurseManage
{
    public class NurseManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NurseManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "NurseManage_default",
                "NurseManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}