using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.DRGManage
{
    public class DRGManageAreaRegistration : AreaRegistration
    {
        public override string AreaName => "DRGManage";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              this.AreaName + "_Default",
              this.AreaName + "/{controller}/{action}/{id}",
              new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
              new string[] { "Newtouch.HIS.Web.Areas." + this.AreaName + ".Controllers"
              ,"FrameworkBase.MultiOrg.Web.Areas.DRGManage.Controllers"}
            );
        }
    }
}