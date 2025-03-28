using Autofac;
using Autofac.Integration.WebApi;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Interface;
using Newtouch.Core.NLogger;
using Newtouch.Core.Redis.Cache;
using Newtouch.HIS.Repository;
using System.Reflection;
using System.Web.Http;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.API.Common.Interface;
using Newtouch.HIS.API.Common.Models;

namespace Newtouch.PDS.API.App_Start
{
    public class DependencyResolver
    {
        /// <summary>
        /// 注入Web API控制器/过滤器
        /// </summary>
        public static void RegisteWebApiComponent()
        {
            var builder = new ContainerBuilder();

            //注册控制器
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //注册默认EF上下文
            builder.RegisterType<DefaultDatabaseFactory>().As<IDefaultDatabaseFactory>().InstancePerLifetimeScope();

            //日志注册
            //LoggerHelper.InitLog4net("/Configs/log4net.config");
            builder.RegisterType<NLogger>().As<ILogger>().InstancePerLifetimeScope();

            ////缓存
            builder.RegisterType<SERedisCache>().As<ICache>().InstancePerLifetimeScope();

            //API用户身份解析器
            builder.RegisterType<HISAPIUserIdentityResolver>().As<IUserIdentityResolver<Identity>>().InstancePerLifetimeScope();

            #region IOC注册底层
            builder.RegisterAssemblyTypes(typeof(RepositoryBase<>).Assembly).Where(t => t.Name.EndsWith("Repo")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(DmnServiceBase).Assembly).Where(t => t.Name.EndsWith("DmnService")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(FrameworkBase.MultiOrg.Application.AppBase).Assembly).Where(t => t.Name.EndsWith("App")).AsImplementedInterfaces().InstancePerLifetimeScope();

            #endregion

            #region IOC, then registe yours
            //DLL所在的绝对路径
            var repoAssembly = Assembly.Load("Newtouch.HIS.Repository");
            builder.RegisterAssemblyTypes(repoAssembly).Where(t => t.Name.EndsWith("Repo") || t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            var dmnserviceAssembly = Assembly.Load("Newtouch.HIS.DomainServices");
            builder.RegisterAssemblyTypes(dmnserviceAssembly).Where(t => t.Name.EndsWith("DmnService")).AsImplementedInterfaces().InstancePerLifetimeScope();


            var appAssembly = Assembly.Load("Newtouch.HIS.Application");
            builder.RegisterAssemblyTypes(appAssembly).Where(t => t.Name.EndsWith("App")).Where(t => !string.IsNullOrEmpty(t.Namespace)).AsImplementedInterfaces().InstancePerLifetimeScope();
            #endregion

            //注册Autofac过滤器提供程序.
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            // 设置依赖解析器-Dependency Resolver
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            // Configure Deafult Instance Resolver
            DependencyDefaultInstanceResolver.Register(interfaceType =>
            {
                return FrameworkBase.API.ApiControllerBaseEx.ResolveService(interfaceType);
            });
        }
    }
}