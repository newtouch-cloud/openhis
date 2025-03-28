using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.NonTreatmentItemManage
{
    public class NonTreatmentItemManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NonTreatmentItemManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "NonTreatmentItemManage_default",
                "NonTreatmentItemManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            //context.MapRoute(
            //  this.AreaName + "_Default",
            //  this.AreaName + "/{controller}/{action}/{id}",
            //  new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
            //  new string[] { "Newtouch.HIS.Web.Areas." + this.AreaName + ".Controllers" }
            //);
        }
    }
}