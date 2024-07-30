using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.QueueManage
{
    public class QueueManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "QueueManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "QueueManage_default",
                "QueueManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}