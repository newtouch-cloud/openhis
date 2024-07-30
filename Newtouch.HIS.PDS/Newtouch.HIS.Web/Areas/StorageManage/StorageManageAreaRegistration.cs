using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.StorageManage
{
    public class StorageManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "StorageManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "StorageManage_default",
                "StorageManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}