using Newtouch.Core.Common;
using Newtouch.HIS.Base.HOSP.API.App_Start;
using Newtouch.HIS.Domain.DBContext.Infrastructure;
using Newtouch.Infrastructure;
using System;
using System.Web;
using System.Web.Http;

namespace Newtouch.HIS.Base.HOSP.API
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
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //注册 释放数据库链接
            FrameworkBase.API.ApiControllerBaseEx.RegisterDBContextDisposer(() =>
            {
                //释放DefaultDbContext
                Newtouch.Infrastructure.EF.DatabaseFactoryDisposeHelper.TryDisposeDBContext<IBaseDatabaseFactory, BaseDbContext>();
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