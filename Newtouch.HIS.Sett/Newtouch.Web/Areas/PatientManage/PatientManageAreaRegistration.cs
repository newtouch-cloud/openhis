using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.PatientManage
{
    public class PatientManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "PatientManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                this.AreaName + "_Default",
                this.AreaName + "/{controller}/{action}/{id}",
                new { area = this.AreaName, controller = "HospiterRes", action = "Index", id = UrlParameter.Optional },
                new string[] { "Newtouch.HIS.Web.Areas." + this.AreaName + ".Controllers" }
            );
        }
    }
}