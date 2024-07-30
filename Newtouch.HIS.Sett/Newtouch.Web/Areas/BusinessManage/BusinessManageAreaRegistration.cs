using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.BusinessManage
{
    public class BusinessManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BusinessManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "BusinessManage_default",
                "BusinessManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}