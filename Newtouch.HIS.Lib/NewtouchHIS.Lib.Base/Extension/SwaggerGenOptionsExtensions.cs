using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NewtouchHIS.Lib.Base.Extension
{
    public static class SwaggerGenOptionsExtensions
    {
        /// <summary>
        /// 为swagger增加Authentication报文头
        /// </summary>
        /// <param name="option"></param>
        public static void AddAuthenticationHeader(this SwaggerGenOptions option)
        {
            option.AddSecurityDefinition("Authorization",
                new OpenApiSecurityScheme
                {
                    Description = "Authorization header. \r\nExample:Bearer 12345ABCDE",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Authorization"
                }
                ); ;

            option.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference=new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Authorization"
                        },
                        Scheme="oauth2",
                        Name="Authorization",
                        In=ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        }
    }
}
