using System.Web.Mvc;

namespace Newtouch.MR.ManageSystem.Web.Areas.MRHomePage
{
    public class MRHomePageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MRHomePage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MRHomePage_default",
                "MRHomePage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
                , new[] { "Newtouch.MR.ManageSystem.Web.Areas.MRHomePage.Controllers", "FrameworkBase.MultiOrg.Web.Areas.MRHomePage.Controllers" } //指定命名空间
            );

        }
    }
}