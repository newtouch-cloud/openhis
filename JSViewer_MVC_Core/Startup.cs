using System;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using GrapeCity.ActiveReports.Aspnetcore.Viewer;
using GrapeCity.ActiveReports.Aspnetcore.Designer;
using System.IO;
using JSViewer_MVC_Core.Services;
using JSViewer_MVC_Core.Implementation.Storage;
using JSViewer_MVC_Core.Implementation.CustomStore;
using JSViewer_MVC_Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using JSViewer_MVC_Core.Code;
using JSViewer_MVC_Core.Bll;
using JSViewer_MVC_Core.Bll.OpenSource;

namespace JSViewer_MVCCore
{
    public class Startup
    {
        /// <summary>
        /// lite 数据库文件位置
        /// </summary>
        private static readonly string ResourcesRoot = "Resources/";// Path.Combine(Directory.GetCurrentDirectory(), "Resources");

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            new AppSettingHelper(Configuration);
            services
                .AddLogging(config =>
                {
                    // Disable the default logging configuration
                    config.ClearProviders();

                    // Enable logging for debug mode only
                    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
                    {
                        config.AddConsole();
                    }
                })
                .AddReporting()
                .AddDesigner()
                .AddSingleton<SysReportTemplateBll>()
                .AddSingleton<SysReportTemplatesBll>()
                .AddSingleton<ICustomStorage>(s => new JSViewer_MVC_Core.Implementation.Storage.LiteDB(Path.Combine(ResourcesRoot, "lite.db"),s.GetRequiredService<SysReportTemplateBll>(), s.GetRequiredService<SysReportTemplatesBll>()))
                .AddSingleton<ICustomStoreService>(s => new CustomStoreService(s.GetRequiredService<ICustomStorage>()))
                
                .AddMvc(options => options.EnableEndpointRouting = false)
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            //services
            //   .AddLogging(config =>
            //   {
            //       // Disable the default logging configuration
            //       config.ClearProviders();

            //       // Enable logging for debug mode only
            //       if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
            //       {
            //           config.AddConsole();
            //       }
            //   })
            //   .AddReporting()
            //   .AddDesigner()
            //   .AddSingleton<SysReportTemplatesBll>()
            //   .AddSingleton<ICustomStorage>(s => new JSViewer_MVC_Core.Implementation.Storage.LiteDB1(Path.Combine(ResourcesRoot, "lite.db"), s.GetRequiredService<SysReportTemplatesBll>()))
            //   .AddSingleton<ICustomStoreService>(s => new CustomStoreService(s.GetRequiredService<ICustomStorage>()))

            //   .AddMvc(options => options.EnableEndpointRouting = false)
            //   .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            var resourcesService = app.ApplicationServices.GetRequiredService<ICustomStoreService>();

            //配置允许跨域
            app.UseCors(cors => cors.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                .AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithExposedHeaders("Content-Disposition"));

            app.UseReporting(settings =>
            {
                settings.UseCustomStore(resourcesService.GetReport);
                settings.UseCompression = true;
            });

            app.UseDesigner(config =>
            {
                config.UseCustomStore(resourcesService);
            });

            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}