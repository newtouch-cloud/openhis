using System.Web.Mvc;

namespace Newtouch.MR.ManageSystem.Web.Areas.ReportManage
{
    public class ReportManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ReportManage";
            }
        }

        //public override void RegisterArea(AreaRegistrationContext context) 
        //{
        //    context.MapRoute(
        //        "ReportManage_default",
        //        "ReportManage/{controller}/{action}/{id}",
        //        new { action = "Index", id = UrlParameter.Optional }
        //    );
        //}


        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ReportManage_default",
                "ReportManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
                , new[] { "Newtouch.MR.ManageSystem.Web.Areas.ReportManage.Controllers", "FrameworkBase.MultiOrg.Web.Areas.ReportManage.Controllers" } //指定命名空间
            );
        }
    }
}