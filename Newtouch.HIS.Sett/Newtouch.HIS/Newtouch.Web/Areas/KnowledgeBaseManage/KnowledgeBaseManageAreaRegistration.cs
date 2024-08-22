using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.KnowledgeBaseManage
{
    public class KnowledgeBaseManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "KnowledgeBaseManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "KnowledgeBaseManage_default",
                "KnowledgeBaseManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}