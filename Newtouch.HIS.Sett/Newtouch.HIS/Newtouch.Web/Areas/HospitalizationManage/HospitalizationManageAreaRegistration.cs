using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.HospitalizationManage
{
    public class HospitalizationManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HospitalizationManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "HospitalizationManage_default",
                "HospitalizationManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}