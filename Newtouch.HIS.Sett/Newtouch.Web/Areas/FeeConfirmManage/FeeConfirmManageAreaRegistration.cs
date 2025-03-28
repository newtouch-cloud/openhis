using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.FeeConfirmManage
{
    public class FeeConfirmManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "FeeConfirmManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "FeeConfirmManage_default",
                "FeeConfirmManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}