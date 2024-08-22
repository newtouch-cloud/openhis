using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.OutPatientManage
{
    public class OutpatientManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "OutpatientManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "OutpatientManage_default",
                "OutpatientManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}