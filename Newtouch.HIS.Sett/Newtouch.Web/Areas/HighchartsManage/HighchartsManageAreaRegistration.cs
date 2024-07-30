using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.HighchartsManage
{
    public class HighchartsManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "HighchartsManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              "HighchartsManage_default",
             "HighchartsManage/{controller}/{action}/{id}",
               new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
