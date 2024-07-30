using Newtouch.HIS.Domain.DBContext.Infrastructure;
using Newtouch.HIS.Web.Core.HttpModules;
using Newtouch.Infrastructure;
using System.Web.Mvc;
using System.Web.Routing;

namespace Newtouch.HIS.Base
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 启动应用程序
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteRegistration.RegisterRoutes(RouteTable.Routes);

            App_Start.DependencyResolver.RegisteWebApiComponent();

            //数据库链接释放
            AspNetObjectContextDisposalModule.Register(() =>
            {
                Infrastructure.EF.DatabaseFactoryDisposeHelper.TryDisposeDBContext<IBaseDatabaseFactory, BaseDbContext>();
            });

        }

    }
}