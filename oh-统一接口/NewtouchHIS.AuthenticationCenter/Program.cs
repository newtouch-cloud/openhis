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

//��������ע��������ݿ�
builder.Services.AddDependencyService();

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
            //��ʹ���շ���ʽ��key
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


#region ��Ȩ��Ȩ
app.UseAuthentication();        //���jwt��Ȩ 
//������ϵ���ִ�У��м���Լ��жϣ� Ȩ����������
app.UseAuthorization();   //��Ȩ
#endregion

app.MapControllers();

app.Run();
