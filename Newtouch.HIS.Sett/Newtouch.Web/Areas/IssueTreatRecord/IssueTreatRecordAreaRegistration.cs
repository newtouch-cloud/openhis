using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.IssueTreatRecord
{
    public class IssueTreatRecordAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "IssueTreatRecord";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "IssueTreatRecord_default",
                "IssueTreatRecord/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}