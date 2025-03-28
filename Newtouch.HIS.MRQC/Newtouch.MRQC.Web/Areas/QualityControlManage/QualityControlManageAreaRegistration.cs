using System.Web.Mvc;

namespace Newtouch.MRQC.Web.Areas.QualityControlManage
{
    public class QualityControlManageAreaRegistration: AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "QualityControlManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                this.AreaName + "_Default",
                this.AreaName + "/{controller}/{action}/{id}",
                new { area = this.AreaName, controller = "SysItem", action = "Index", id = UrlParameter.Optional },
                new string[] { "Newtouch.MRQC.Web.Areas." + this.AreaName + ".Controllers" }
            );
        }
    }
}