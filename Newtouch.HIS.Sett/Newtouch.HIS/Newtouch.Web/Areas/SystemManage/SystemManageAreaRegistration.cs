using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.SystemManage
{
    public class SystemManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SystemManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              this.AreaName + "_Default",
              this.AreaName + "/{controller}/{action}/{id}",
              new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
              new string[] { "Newtouch.HIS.Web.Areas." + this.AreaName + ".Controllers"
              ,"FrameworkBase.MultiOrg.Web.Areas.SystemManage.Controllers"}
            );
        }
    }
}
