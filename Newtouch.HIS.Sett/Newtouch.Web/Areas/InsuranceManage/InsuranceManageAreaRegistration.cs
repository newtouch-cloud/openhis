using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.InsuranceManage
{
    public class InsuranceManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "InsuranceManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "InsuranceManage_default",
                "InsuranceManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}