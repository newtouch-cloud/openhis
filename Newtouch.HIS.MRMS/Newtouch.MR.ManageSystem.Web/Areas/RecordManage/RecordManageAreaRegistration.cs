using System.Web.Mvc;

namespace Newtouch.MR.ManageSystem.Web.Areas.RecordManage
{
    public class RecordManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RecordManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RecordManage_default",
                "RecordManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
                , new[] { "Newtouch.MR.ManageSystem.Web.Areas.RecordManage.Controllers", "FrameworkBase.MultiOrg.Web.Areas.RecordManage.Controllers" } //指定命名空间
            );

        }
    }
}