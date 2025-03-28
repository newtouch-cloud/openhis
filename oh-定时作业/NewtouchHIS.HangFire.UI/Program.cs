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
//    x.SetDataCompatibilityLevel(CompatibilityLevel.Version_180); //�������ݼ����Լ���
//    x.UseSimpleAssemblyNameTypeSerializer();
//    x.UseRecommendedSerializerSettings();
//    x.UseDashboardMetric(DashboardMetrics.ServerCount)//����������
//       .UseConsole()
//       .UseManagementPages(Assembly.Load("NewtouchHIS.HangFire.UI")); //���س���
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
////����hangfire dashboard�м��
//app.UseHangfireDashboard("/hangfire", new DashboardOptions
//{
//    Authorization = new[] { new HisAuthorizationFilter() },
//    DashboardTitle = "��ԴHIS.������ȹ���", //ҳ�����
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
