using FrameworkBase.MultiOrg.Application.Interface;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DBContext.Infrastructure;
using Newtouch.HIS.Web.Core.HttpModules;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace Newtouch.HIS.Web
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
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            App_Start.DependencyResolver.RegisteWebApiComponent();

            //向上下文注册表
            DefaultDbContext.PartialTBRegister(DefaultDbContextTBRegister.Registe);

            //数据库链接释放
            AspNetObjectContextDisposalModule.Register(() =>
            {
                Newtouch.Infrastructure.EF.DatabaseFactoryDisposeHelper.TryDisposeDBContext<IDefaultDatabaseFactory, DefaultDbContext>();
            });

            //权限验证向下层注册
            HandlerAuthorizeAttribute.Register((IList<string> roleIdList, string moduleId, string action) =>
            {
                return DependencyDefaultInstanceResolver.GetInstance<IRoleAuthorizeApp>().ActionValidate(roleIdList, action);
            });

        }

    }
}