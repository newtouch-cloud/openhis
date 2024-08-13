using NewtouchHIS.AuthenticationCenter.Services;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Utilities;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using NewtouchHIS.Lib.Base.Filter;
using NewtouchHIS.Lib.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigInitHelper.SysConfig = builder.Configuration.GetSection("BusinessConfig").Get<BusinessConfig>();
ConfigInitHelper.DbConfig = builder.Configuration.GetSection("BusinessDB").Get<BusinessDB>();
builder.Services.AddSingleton(new RedisHelper(builder.Configuration.GetSection("RedisConfig")));

//配置依赖注入访问数据库
builder.Services.AddDependencyService();

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
            //不使用驼峰样式的key
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        }
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGenNewtonsoftSupport();
var jwtOptions = builder.Configuration.GetSection("JWT");
builder.Services.Configure<JWTOptions>(jwtOptions);
builder.Services.AddTransient<IJWTService, HSJWTService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


#region 鉴权授权
app.UseAuthentication();        //添加jwt鉴权 
//代码从上到下执行，中间可以加判断， 权限设置问题
app.UseAuthorization();   //授权
#endregion

app.MapControllers();

app.Run();
