using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.DaySettleManage
{
    public class DaySettleManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DaySettleManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DaySettleManage_default",
                "DaySettleManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}