using Autofac;
using Autofac.Integration.Mvc;
using Newtouch.Common;
using Newtouch.Core.Common.Interface;
using Newtouch.Core.Logger;
using Newtouch.Core.Redis.Cache;
using Newtouch.HIS.Application.SystemSecurity;
using Newtouch.HIS.Domain.DBContext.Infrastructure;
using Newtouch.HIS.DomainServices;
using Newtouch.HIS.Repository;
using Newtouch.Infrastructure;
using System.Reflection;

namespace Newtouch.HIS.Sett.Rehab.App_Start
{
    /// <summary>
    /// 依赖注入配置
    /// </summary>
    public class DependencyResolver
    {
        /// <summary>
        /// 
        /// </summary>
        public static void RegisteWebApiComponent()
        {
            var builder = new ContainerBuilder();

            //注册控制器
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            //注册默认EF上下文
            builder.RegisterType<NewtouchDatabaseFactory>().As<INewtouchDatabaseFactory>().InstancePerLifetimeScope();
            //builder.RegisterType<BaseDatabaseFactory>().As<IBaseDatabaseFactory>().InstancePerLifetimeScope();

            //日志注册
            LoggerHelper.InitLog4net("/Configs/log4net.config");
            builder.RegisterType<Log4NetLogger>().As<ILogger>().InstancePerLifetimeScope();

            //缓存
            builder.RegisterType<SERedisCache>().As<ICache>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(RepositoryBase<>).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(RepositoryBase<>).Assembly)
                .Where(t => t.Name.EndsWith("Repo"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(BookkeepInHosDmnService).Assembly)
                .Where(t => t.Name.EndsWith("DmnService"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(DbBackupApp).Assembly)
                .Where(t => t.Name.EndsWith("App"))
                .Where(t => !string.IsNullOrEmpty(t.Namespace))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterFilterProvider();
            IContainer container = builder.Build();
            System.Web.Mvc.DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // Configure Deafult Instance Resolver
            DependencyDefaultInstanceResolver.Register(interfaceType =>
            {
                return System.Web.Mvc.DependencyResolver.Current.GetService(interfaceType);
            });

        }
    }
}