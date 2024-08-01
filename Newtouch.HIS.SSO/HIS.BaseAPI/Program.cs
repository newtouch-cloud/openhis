using Microsoft.AspNetCore.Mvc.Authorization;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Filter;
using NewtouchHIS.Lib.Base.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInterfaceServiceBase(builder.Configuration);
builder.Services.AddControllers();
//���п��������������֤
builder.Services.AddMvc(options =>
{
    options.Filters.Add(new AppResultFilter());
    options.Filters.Add(typeof(LogTrackingAttribute));
    options.Filters.Add(new AuthorizeFilter());//����MVC����Ĭ�������Ȩ��ǩ
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwagger();
builder.Services.AddSwaggerGenNewtonsoftSupport();
var jwtOptions = builder.Configuration.GetSection("SSOAuthIdentity")?.Get<List<SSOAuthIdentity>>();
if (jwtOptions != null)
{
    builder.Services.JWTAuthentication(true, jwtOptions?.FirstOrDefault());
}
else
{
    Console.WriteLine("Warnning:SSOAuthIdentity �����쳣");
}
builder.Services.AddCorsService();

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
#endregion
app.UseAuthorization();
app.UseNewtouchExceptionHandler();
app.MapControllers();
app.UseCors("Cors");
app.Run();
