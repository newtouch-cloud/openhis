using Newtouch.Herp.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Application.Interface;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.HIS.Web.Core.HttpModules;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Newtouch.Herp.Infrastructure;

namespace Newtouch.Herp.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 启动应用程序
        /// </summary>
        protected void Application_Start()
        {
            //调用 AreaRegistration.RegisterAllAreas 方法让MVC应用程序在启动后会寻找所有继承自 AreaRegistration 的类，并为每个这样的类调用它们的 RegisterArea 方法
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
                Newtouch.Infrastructure.EF.DatabaseFactoryDisposeHelper.TryDisposeDBContext<IBaseDatabaseFactory, BaseDatabaseFactory>();
            });

            //权限验证向下层注册
            HandlerAuthorizeAttribute.Register((IList<string> roleIdList, string moduleId, string action) =>
            {
                return DependencyDefaultInstanceResolver.GetInstance<IRoleAuthorizeApp>().ActionValidate(roleIdList, action);
            });


        }

    }
}