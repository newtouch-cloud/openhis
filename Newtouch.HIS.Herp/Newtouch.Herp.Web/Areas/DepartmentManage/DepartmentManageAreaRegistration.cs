using System.Web.Mvc;

namespace Newtouch.Herp.Web.Areas.DepartmentManage
{
    public class DepartmentManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DepartmentManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DepartmentManage_default",
                "DepartmentManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}