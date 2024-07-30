using Autofac;
using Autofac.Integration.Mvc;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Interface;
using Newtouch.Core.NLogger;
using Newtouch.Core.Redis.Cache;
using System.Reflection;
using System.Web;

namespace Newtouch.EMR.Web.App_Start
{
    /// <summary>
    /// 依赖注入配置
    /// </summary>
    public class DependencyResolver
    {
        /// <summary>
        /// 依赖注入配置
        /// </summary>
        public static void RegisteWebApiComponent()
        {
            var builder = new ContainerBuilder();

            //注册默认EF上下文
            builder.RegisterType<DefaultDatabaseFactory>().As<IDefaultDatabaseFactory>().InstancePerLifetimeScope();

            //注册控制器
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.Load("FrameworkBase.MultiOrg.Web"));

            //日志注册
            builder.RegisterType<NLogger>().As<ILogger>().InstancePerLifetimeScope();

            //缓存
            builder.RegisterType<SERedisCache>().As<ICache>().InstancePerLifetimeScope();

            #region IOC注册底层
            builder.RegisterAssemblyTypes(typeof(FrameworkBase.MultiOrg.Repository.RepositoryBase<>).Assembly)
                .Where(t => t.Name.EndsWith("Repo"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(FrameworkBase.MultiOrg.DmnService.DmnServiceBase).Assembly)
                .Where(t => t.Name.EndsWith("DmnService"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(FrameworkBase.MultiOrg.Application.AppBase).Assembly)
                .Where(t => t.Name.EndsWith("App"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            #endregion

            #region IOC, then registe yours
            //DLL所在的绝对路径
            var repoAssembly = Assembly.Load("Newtouch.EMR.Repository");
            builder.RegisterAssemblyTypes(repoAssembly)
                .Where(t => t.Name.EndsWith("Repo"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            var dmnserviceAssembly = Assembly.Load("Newtouch.EMR.DomainServices");
            builder.RegisterAssemblyTypes(dmnserviceAssembly)
                .Where(t => t.Name.EndsWith("DmnService"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            var appAssembly = Assembly.Load("Newtouch.EMR.Application");
            builder.RegisterAssemblyTypes(appAssembly)
                .Where(t => t.Name.EndsWith("App"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            #endregion

            builder.RegisterFilterProvider();
            IContainer container = builder.Build();
            System.Web.Mvc.DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // Configure Deafult Instance Resolver
            DependencyDefaultInstanceResolver.Register(interfaceType =>
            {
                if (HttpContext.Current != null)
                {
                    return System.Web.Mvc.DependencyResolver.Current.GetService(interfaceType);
                }
                return null;
            });

        }
    }
}