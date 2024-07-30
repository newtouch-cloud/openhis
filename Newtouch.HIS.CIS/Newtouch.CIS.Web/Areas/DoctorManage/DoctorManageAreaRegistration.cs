using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.DoctorManage
{
    public class DoctorManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DoctorManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DoctorManage_default",
                "DoctorManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}