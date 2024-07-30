using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.TemplateManage
{
    public class TemplateManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TemplateManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TemplateManage_default",
                "TemplateManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}