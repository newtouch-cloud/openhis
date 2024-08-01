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

//��������ע��������ݿ�
builder.Services.AddDependencyService();
//���IApiUserSessionʵ����
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
            //����ѭ������
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.Formatting = Formatting.Indented;
            options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss.fff";//ȫ�ִ��� ����ʱ���ʽ
            //options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;//ȫ�ִ��� ����ʱ�䲢�����ػ�����
            //var dateConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter
            //{
            //    DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
            //}

            //options.SerializerSettings.Converters.Add(dateConverter);
            //��ʹ���շ���ʽ��key
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();

            //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();//����ĸСд�շ�ʽ����
            //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; ��ֵ����
        }
);
//���п��������������֤
builder.Services.AddMvc(options =>
{
    options.Filters.Add(new AuthorizeFilter());//����MVC����Ĭ�������Ȩ��ǩ
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddSwaggerGenNewtonsoftSupport();

var jwtOptions = config.GetSection("SSOAuthIdentity")?.Get<List<SSOAuthIdentity>>();
builder.Services.JWTAuthentication(true, jwtOptions?.FirstOrDefault());

builder.Services.AddTransient<IDrgGroupService, DrgGroupService>();
builder.Services.AddTransient<IChsDrg11Policy, ChsDrg11Policy>();
builder.Services.AddTransient<IDrgWuhan2022Policy, DrgWuhan2022Policy>();

//���ÿ������
builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors", p =>
    {
        p.WithOrigins(ConfigInitHelper.SysConfig.CorsWithOrigins!.Split(',')).AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();

    });
});
//���SignalRע������
//builder.Services.AddOhSignalR();
builder.Services.AddSignalR(o =>
{
    o.EnableDetailedErrors = true;//��ϸ�쳣��Ϣ
    o.HandshakeTimeout = TimeSpan.FromSeconds(30);//���ֳ�ʱʱ��
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#region jwt
app.UseAuthentication();        //���jwt��Ȩ 
//������ϵ���ִ�У��м���Լ��жϣ� Ȩ����������
app.UseAuthorization();   //��Ȩ
#endregion
app.UseNewtouchExceptionHandler();
app.MapControllers();
app.UseCors("Cors");
//hubs/msgcenter
app.MapHub<HisNoticeHub>(config.GetSection("MessageCenter:Hub").Get<string>()).RequireCors("Cors"); ;

app.Run();
