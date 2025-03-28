using System.Web.Mvc;

namespace Newtouch.Herp.Web.Areas.DeliveryManage
{
    public class DeliveryManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DeliveryManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DeliveryManage_default",
                "DeliveryManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}