using System;
using System.Collections.Generic;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.DBContext.Infrastructure;

namespace Newtouch.HIS.Sett.Rehab
{
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Start(object sender, EventArgs e)
        {
            System.Web.Mvc.AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(System.Web.Mvc.GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(System.Web.Routing.RouteTable.Routes);

            App_Start.DependencyResolver.RegisteWebApiComponent();

            //数据库链接释放
            Web.Core.HttpModules.AspNetObjectContextDisposalModule.Register(() =>
            {
                Infrastructure.EF.DatabaseFactoryDisposeHelper.TryDisposeDBContext<INewtouchDatabaseFactory, NewtouchDbContext>();
                //Infrastructure.EF.DatabaseFactoryDisposeHelper.TryDisposeDBContext<IBaseDatabaseFactory, BaseDbContext>();
            });

            //权限验证向下层注册
            System.Web.Mvc.HandlerAuthorizeAttribute.Register((IList<string> roleIdList, string moduleId, string action) =>
            {
                return Common.DependencyDefaultInstanceResolver.GetInstance<Application.IRoleAuthorizeApp>().ActionValidate(roleIdList, moduleId, action);
            });
        }

    }
}