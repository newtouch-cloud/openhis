using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.HIS.API.Common.HttpModules;
using Newtouch.HIS.Domain.DBContext.Infrastructure;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebGrease.Configuration;

namespace Newtouch.PDS.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            //向上下文注册表
            DefaultDbContext.PartialTBRegister(DefaultDbContextTBRegister.Registe);

            //注册 释放数据库链接
            FrameworkBase.API.ApiControllerBaseEx.RegisterDBContextDisposer(() =>
            {
                //释放DefaultDbContext
                var key = "AspNetObjectContextDisposalModule_Dispose_Flag_" + typeof(DefaultDbContext).Name;
                if (HttpContext.Current == null && HttpContext.Current.Items[key] == null) return;
                var dbFactory = DependencyDefaultInstanceResolver.GetInstance<IDefaultDatabaseFactory>();
                if (dbFactory != null)
                {
                    dbFactory.Dispose();
                }
            });

            IEntityEx.RegisterCurUserCodeGetter(() =>
            {
                //在基类认证用户身份时写上的
                var account = HttpContext.Current.Items["API_UserIdentity_Account"];
                return account == null ? null : account.ToString();
            });
        }
    }
}
