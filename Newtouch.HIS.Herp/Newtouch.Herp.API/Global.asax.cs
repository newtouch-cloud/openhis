using Newtouch.Herp.API.App_Start;
using Newtouch.Herp.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using System;
using System.Web;
using System.Web.Http;

namespace Newtouch.Herp.API
{
    /// <summary>
    /// 
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //向上下文注册表
            DefaultDbContext.PartialTBRegister(DefaultDbContextTBRegister.Registe);

            //注册 释放数据库链接
            FrameworkBase.API.ApiControllerBaseEx.RegisterDBContextDisposer(()=> {
                //释放DefaultDbContext
                var key = "AspNetObjectContextDisposalModule_Dispose_Flag_" + typeof(DefaultDbContext).Name;
                if (HttpContext.Current != null || HttpContext.Current.Items[key] != null)
                {
                    var dbFactory = DependencyDefaultInstanceResolver.GetInstance<IDefaultDatabaseFactory>();
                    if (dbFactory != null)
                    {
                        dbFactory.Dispose();
                    }
                }
            });

            IEntityEx.RegisterCurUserCodeGetter(() =>
            {
                //在基类认证用户身份时写上的
                var account = HttpContext.Current.Items["API_UserIdentity_Account"];
                return account == null ? null : account.ToString();
            });

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}