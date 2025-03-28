using System.Web.Mvc;
using System.Web.Routing;

namespace Newtouch.HIS.Base.HOSP
{
    /// <summary>
    /// 路由注册
    /// </summary>
    public class RouteRegistration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional },namespaces:new string[]{ "Newtouch.HIS.Base.HOSP.Controllers" }
            );
        }
    }

}