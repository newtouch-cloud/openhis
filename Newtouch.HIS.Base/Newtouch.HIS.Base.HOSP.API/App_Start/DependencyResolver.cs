using Autofac;
using Autofac.Integration.WebApi;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Interface;
using Newtouch.Core.NLogger;
using Newtouch.Core.Redis.Cache;
using Newtouch.HIS.API.Common.Interface;
using Newtouch.HIS.API.Common.Models;
using Newtouch.HIS.Application.Implementation;
using Newtouch.HIS.Domain.DBContext.Infrastructure;
using Newtouch.HIS.DomainServices;
using Newtouch.HIS.Repository;
using Newtouch.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Newtouch.HIS.Base.HOSP.API.App_Start
{
    /// <summary>
    /// 
    /// </summary>
    public class DependencyResolver
    {
        /// <summary>
        /// 注入Web API控制器/过滤器
        /// </summary>
        /// <param name="config">HttpConfiguration</param>
        public static void RegisteWebApiComponent()
        {
            var builder = new ContainerBuilder();

            // 注册WebAPI控制器
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //注册默认EF上下文
            builder.RegisterType<BaseDatabaseFactory>().As<IBaseDatabaseFactory>().InstancePerLifetimeScope();

            //日志注册
            builder.RegisterType<NLogger>().As<ILogger>().InstancePerLifetimeScope();

            //缓存
            builder.RegisterType<SERedisCache>().As<ICache>().InstancePerLifetimeScope();

            //API用户身份解析器
            builder.RegisterType<HISAPIUserIdentityResolver>().As<IUserIdentityResolver<Identity>>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(SysUserRoleRepo).Assembly)
                .Where(t => t.Name.EndsWith("Repository") || t.Name.EndsWith("Repo"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(SysUserDmnService).Assembly)
                .Where(t => t.Name.EndsWith("DmnService"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(SysUserApp).Assembly)
                .Where(t => t.Name.EndsWith("App"))
                .Where(t => !string.IsNullOrEmpty(t.Namespace))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

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