using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.OutOrInStoredManage
{
    public class OutOrInStoredManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "OutOrInStoredManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "OutOrInStoredManage_default",
                "OutOrInStoredManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}