using System.Web.Mvc;

namespace Newtouch.Herp.Web.Areas.SupplierManage
{
    public class SupplierManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SupplierManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SupplierManage_default",
                "SupplierManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}