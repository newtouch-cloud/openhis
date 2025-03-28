using Hangfire;
using NewtouchHIS.HangFire.Core;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.HangFire.UI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
ConfigInitHelper.SysConfig = builder.Configuration.GetSection("BusinessConfig").Get<BusinessConfig>();

//builder.Services.AddHangfire(x =>
//{
//    x.UseRedisStorage("127.0.0.1:6379", new Hangfire.Redis.RedisStorageOptions
//    {
//        Db = 11,
//        Prefix = "OpenHis.Hangfire:"
//    });
//    x.SetDataCompatibilityLevel(CompatibilityLevel.Version_180); //设置数据兼容性级别
//    x.UseSimpleAssemblyNameTypeSerializer();
//    x.UseRecommendedSerializerSettings();
//    x.UseDashboardMetric(DashboardMetrics.ServerCount)//服务器数量
//       .UseConsole()
//       .UseManagementPages(Assembly.Load("NewtouchHIS.HangFire.UI")); //加载程序集
//});
//// Add the processing server as IHostedService
//builder.Services.AddHangfireServer();

builder.Services.AddSelfHangfire(builder.Configuration); 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
////启用hangfire dashboard中间件
//app.UseHangfireDashboard("/hangfire", new DashboardOptions
//{
//    Authorization = new[] { new HisAuthorizationFilter() },
//    DashboardTitle = "开源HIS.任务调度管理", //页面标题
//});

app.ConfigureSelfHangfire(builder.Configuration);

app.UseRouting();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHangfireDashboard();
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
