using FrameworkBase.MultiOrg.Application.Interface;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Domain.DBContext.Infrastructure;
using Newtouch.HIS.Web.Core.HttpModules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Newtouch.CIS.Web
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
            });

            //权限验证向下层注册
            HandlerAuthorizeAttribute.Register((IList<string> roleIdList, string moduleId, string action) =>
            {
                return DependencyDefaultInstanceResolver.GetInstance<IRoleAuthorizeApp>().ActionValidate(roleIdList, action);
            });
        }
        /// <summary>
        /// 捕获全局异常
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();
            string ip = Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR") == null ?
                Request.ServerVariables.Get("Remote_Addr").ToString().Trim() :
                Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR").ToString().Trim();
            string logpath = Server.MapPath("D:/NetLogs/" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Format("==========  {0} Application_Error BEGIN ==========", DateTime.Now));
            builder.AppendLine("Ip:" + ip);
            builder.AppendLine("浏览器:" + Request.Browser.Browser.ToString());
            builder.AppendLine("浏览器版本:" + Request.Browser.MajorVersion.ToString());
            builder.AppendLine("操作系统:" + Request.Browser.Platform.ToString());
            builder.AppendLine("页面：" + Request.Url.ToString());
            builder.AppendLine("错误信息：" + ex.Message);
            builder.AppendLine("错误源：" + ex.Source);
            builder.AppendLine("异常方法：" + ex.TargetSite);
            builder.AppendLine("堆栈信息：" + ex.StackTrace);
            builder.AppendLine("==========  Application_Error END  ===================");

            lock (logpath)
            {
                try
                {
                    using (var writer = new StreamWriter(logpath, true))
                    {
                        writer.Write(builder.ToString());
                    }
                }
                catch
                {
                    // 防止写文件时，文件被人为打开无法写入等
                    // 记录日志报错不做处理，不应影响用户继续使用
                }
            }

            Server.ClearError();
            Response.Redirect("~/Error.htm");
        }

    }
}