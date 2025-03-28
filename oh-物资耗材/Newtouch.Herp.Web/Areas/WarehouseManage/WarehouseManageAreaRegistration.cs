using System.Web.Mvc;

namespace Newtouch.Herp.Web.Areas.WarehouseManage
{
    public class WarehouseManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WarehouseManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WarehouseManage_default",
                "WarehouseManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}