using Newtonsoft.Json;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using Newtonsoft.Json.Serialization;
using NewtouchHIS.Lib.Base.Filter;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Services;
using NewtouchHIS.Lib.Services.HttpService;
using Microsoft.AspNetCore.Mvc.Authorization;
using NewtouchHIS.Lib.Services.DrgGroup;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Services.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLibLogging();
builder.Services.AddHttpContextAccessor();

var config = builder.Configuration;
ConfigInitHelper.DbConfig = config.GetSection("BusinessDB").Get<BusinessDB>();
ConfigInitHelper.SysConfig = config.GetSection("BusinessConfig").Get<BusinessConfig>();
builder.Services.AddSingleton(new RedisHelper(config.GetSection("RedisConfig")));
builder.Services.AddSingleton(new AppSettings(builder.Configuration));

//配置依赖注入访问数据库
builder.Services.AddDependencyService();
//添加IApiUserSession实现类
builder.Services.AddSingleton<IIdentityCache, IdentityCachePrincipal>();
builder.Services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
builder.Services.AddSingleton<IHttpWebRequestHelper, HttpWebRequestHelper>();

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
    options.Filters.Add(typeof(AppResultFilter));
    options.Filters.Add(typeof(LogTrackingAttribute));
}).AddNewtonsoftJson(
        options =>
        {
            //忽略循环引用
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.Formatting = Formatting.Indented;
            options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss.fff";//全局处理 返回时间格式
            //options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;//全局处理 接收时间并做本地化处理
            //var dateConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter
            //{
            //    DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
            //}

            //options.SerializerSettings.Converters.Add(dateConverter);
            //不使用驼峰样式的key
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();

            //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();//首字母小写驼峰式命名
            //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; 空值处理
        }
);
//所有控制器启动身份验证
builder.Services.AddMvc(options =>
{
    options.Filters.Add(new AuthorizeFilter());//所有MVC服务默认添加授权标签
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddSwaggerGenNewtonsoftSupport();

var jwtOptions = config.GetSection("SSOAuthIdentity")?.Get<List<SSOAuthIdentity>>();
builder.Services.JWTAuthentication(true, jwtOptions?.FirstOrDefault());

builder.Services.AddTransient<IDrgGroupService, DrgGroupService>();
builder.Services.AddTransient<IChsDrg11Policy, ChsDrg11Policy>();
builder.Services.AddTransient<IDrgWuhan2022Policy, DrgWuhan2022Policy>();

//配置跨域服务
builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors", p =>
    {
        p.WithOrigins(ConfigInitHelper.SysConfig.CorsWithOrigins!.Split(',')).AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();

    });
});
//添加SignalR注入配置
//builder.Services.AddOhSignalR();
builder.Services.AddSignalR(o =>
{
    o.EnableDetailedErrors = true;//详细异常信息
    o.HandshakeTimeout = TimeSpan.FromSeconds(30);//握手超时时间
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#region jwt
app.UseAuthentication();        //添加jwt鉴权 
//代码从上到下执行，中间可以加判断， 权限设置问题
app.UseAuthorization();   //授权
#endregion
app.UseNewtouchExceptionHandler();
app.MapControllers();
app.UseCors("Cors");
//hubs/msgcenter
app.MapHub<HisNoticeHub>(config.GetSection("MessageCenter:Hub").Get<string>()).RequireCors("Cors"); ;

app.Run();
