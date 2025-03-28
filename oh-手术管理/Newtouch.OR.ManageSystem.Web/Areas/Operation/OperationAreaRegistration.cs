using System.Web.Mvc;

namespace Newtouch.OR.ManageSystem.Web.Areas.Operation
{
    public class OperationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Operation";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //context.MapRoute(
            //    "Operation_default",
            //    "Operation/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);

            context.MapRoute(
                "Operation_default",
                "Operation/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
                , new[] { "Newtouch.OR.ManageSystem.Web.Areas.Operation.Controllers", "FrameworkBase.MultiOrg.Web.Areas.Operation.Controllers" } //指定命名空间
            );
        }
    }
}