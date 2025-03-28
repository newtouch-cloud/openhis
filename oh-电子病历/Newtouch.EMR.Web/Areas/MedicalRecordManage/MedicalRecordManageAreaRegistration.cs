using System.Web.Mvc;

namespace Newtouch.EMR.Web.Areas.MedicalRecordManage
{
    public class MedicalRecordManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MedicalRecordManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MedicalRecordManage_default",
                "MedicalRecordManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}