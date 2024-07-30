using AutoMapper;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NewtouchHIS.Lib.Base.Filter;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using SqlSugar;
using System.Net;
using System.Reflection;
using System.Text;

namespace NewtouchHIS.Lib.Base.Extension
{
    public static class ServiceCollectionExt
    {
        #region config
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton(new RedisHelper(configuration.GetSection("RedisConfig")));

            ConfigInitHelper.DbConfig = configuration.GetSection("BusinessDB").Get<BusinessDB>();
            ConfigInitHelper.SysConfig = configuration.GetSection("BusinessConfig").Get<BusinessConfig>();
            #region 测试中 
            //services.AddSingleton(new AppSettings(configuration));
            //ConfigInitHelper.RedisConfig = configuration.GetSection("RedisConfig").Get<RedisDB>();
            #endregion
            return services;
        }

        #endregion

        #region IOC        
        public static IServiceCollection AddDependencyService(this IServiceCollection services)
        {
            #region 依赖注入
            var baseType = typeof(IDependency);
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var getFiles = Directory.GetFiles(path, "*.dll");  //.Where(o=>o.Match())
            var referencedAssemblies = getFiles.Select(Assembly.LoadFrom).ToList();  //.Select(o=> Assembly.LoadFrom(o))         

            //var ss = referencedAssemblies.SelectMany(o => o.GetTypes());
            var types = referencedAssemblies
                        .SelectMany(a => a.DefinedTypes)
                        .Select(type => type.AsType())
                        .Where(x => x != baseType && baseType.IsAssignableFrom(x)).ToList();
            var implementTypes = types.Where(x => x.IsClass).ToList();
            var interfaceTypes = types.Where(x => x.IsInterface).ToList();
            foreach (var implementType in implementTypes)
            {
                if (typeof(IScopedDependency).IsAssignableFrom(implementType))
                {
                    var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                    if (interfaceType != null)
                        services.AddScoped(interfaceType, implementType);
                }
                else if (typeof(ISingletonDependency).IsAssignableFrom(implementType))
                {
                    var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                    if (interfaceType != null)
                        services.AddSingleton(interfaceType, implementType);
                }
                else
                {
                    var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                    if (interfaceType != null)
                        services.AddTransient(interfaceType, implementType);
                }
            }
            //AddAutoMapDependencyService(services, referencedAssemblies);
            AddMapSterDependencyService(services, referencedAssemblies);
            return services;
            #endregion
        }

        /// <summary>
        /// automapper 注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoMapDependencyService(this IServiceCollection services, List<Assembly> assemblies)
        {
            var baseType = typeof(Profile);
            var profiles = assemblies.SelectMany(a => a.DefinedTypes)
                        .Select(type => type.AsType()).Where(x => x != baseType && baseType.IsAssignableFrom(x)).ToArray();
            //services.AddAutoMapper(typeof(MappingProfile));
            //services.AddAutoMapper(profiles);
            return services;
        }
        /// <summary>
        /// Mapster注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddMapSterDependencyService(this IServiceCollection services, List<Assembly> assemblies)
        {
            var config = new TypeAdapterConfig();
            // Or
            // var config = TypeAdapterConfig.GlobalSettings;
            services.AddSingleton(config);
            var baseType = typeof(IRegister);
            var profiles = assemblies.SelectMany(a => a.DefinedTypes)
                  .Select(type => type.AsType()).Where(x => x != baseType && baseType.IsAssignableFrom(x)).ToArray();
            config.Scan(assemblies.ToArray());

            //services.AddScoped<IMapper, ServiceMapper>();
            return services;
        }
        #endregion

        #region JWT
        /// <summary>
        /// jwt鉴权逻辑配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="enable"></param>
        /// <param name="jwtOptions"></param>
        /// 授权API 获取负载信息
        /// base.HttpContext.AuthenticatAsync().Result.Principal.Claims
        /// <returns></returns>
        public static IServiceCollection JWTAuthentication(this IServiceCollection services, bool enable, SSOAuthIdentity jwtOptions)
        {
            if (enable)
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,//是否验证发行商
                        ValidateAudience = false,//是否验证受众者
                        ValidateLifetime = true,//是否验证失效时间
                        ValidateIssuerSigningKey = true,//是否验证签名键
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.AppSecret)),
                        AudienceValidator = (m, n, z) => { return m != null; },
                        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => { return true; },
                    };
                    options.Events = new JwtBearerEvents
                    {
                        //OnAuthenticationFailed = context => { 

                        //},
                        //权限验证失败触发
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.ContentType = "application/json";
                            context.Response.StatusCode = StatusCodes.Status200OK;
                            var data = new Result
                            {
                                HttpStatus = HttpStatusCode.Unauthorized,
                                Message = $"token校验失败:{context.Error}",   //{context.AuthenticateFailure?.Message}

                            };
                            context.Response.WriteAsync(data.ToJson());
                            return Task.FromResult(0);
                        },
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Headers["Authorization"].ToString();
                            var path = context.HttpContext.Request.Path;
                            var msgurl = AppSettings.GetConfig("MessageCenter:Hub");
                            if (path.StartsWithSegments(msgurl))
                            {
                                if (string.IsNullOrWhiteSpace(accessToken) && context.Request.Cookies.ContainsKey("accesstoken"))
                                {
                                    var querys = context.Request.Cookies.Where(p => p.Key.Contains("accesstoken")).FirstOrDefault();
                                    accessToken = querys.Value;
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(accessToken) && (path.StartsWithSegments(msgurl)))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            }
            return services;
        }


        #endregion

        #region SqlSugar
        public static IServiceCollection AddSqlsugarSetup(this IServiceCollection services, IConfiguration configuration)
        {
            var BusinessDBConf = configuration.GetSection("BusinessDB").Get<BusinessDB>();
            if (BusinessDBConf != null && BusinessDBConf.DBConf != null && BusinessDBConf.DBConf.Count > 0 && !string.IsNullOrWhiteSpace(BusinessDBConf.MainDB))
            {
                var configConnection = new List<ConnectionConfig>();
                BusinessDBConf.DBConf.ForEach(option =>
                {
                    configConnection.Add(new ConnectionConfig()
                    {
                        ConfigId = option.ConnId/*.ToLower()*/,
                        ConnectionString = option.DBConn,
                        DbType = (DbType)option.DBType,
                        IsAutoCloseConnection = true,
                        MoreSettings = new ConnMoreSettings()
                        {
                            IsAutoRemoveDataCache = true,
                            IsWithNoLockQuery = true,
                            DisableNvarchar = true,
                        }
                        //InitKeyType = InitKeyType.SystemTable
                    });
                });


                services.AddSingleton<ISqlSugarClient>(o =>
                {
                    //多租户循环添加AOP
                    SqlSugarScope sqlSugar = new SqlSugarScope(configConnection);
                    configConnection.ForEach(c =>
                    {
                        SqlSugarScopeProvider client = sqlSugar.GetConnectionScope(c.ConfigId);
                        //每次Sql执行前事件
                        client.Aop.OnLogExecuting = (sql, pars) =>
                        {
                            //var queryString = new KeyValuePair<string, SugarParameter[]>(sql, pars);
                            //if (sql.StartsWith("UPDATE") || sql.StartsWith("INSERT"))
                            //{
                            //    Console.ForegroundColor = ConsoleColor.Blue;
                            //    Console.WriteLine($"==============新增/修改操作==============");
                            //    Console.WriteLine(ToSqlExplain.GetSql(queryString));
                            //}
                            //if (sql.StartsWith("DELETE"))
                            //{
                            //    Console.ForegroundColor = ConsoleColor.Red;
                            //    Console.WriteLine($"==============删除操作==============");
                            //}
                            //if (sql.StartsWith("SELECT"))
                            //{
                            //    Console.ForegroundColor = ConsoleColor.Green;
                            //    Console.WriteLine($"==============查询操作==============");
                            //}
                            //Console.WriteLine(ToSqlExplain.GetSql(queryString));
                            //Console.ForegroundColor = ConsoleColor.White;
                        };
                        //每次Sql执行后事件
                        client.Aop.OnLogExecuted = (sql, pars) =>
                        {
                            //执行时间超过10秒
                            if (client.Ado.SqlExecutionTime.TotalSeconds > 5)
                            {
                                Console.WriteLine(sql);
                            }
                        };

                        ////SQL报错
                        //client.Aop.OnError = (exp) =>
                        //{
                        //    var data = new Result
                        //    {
                        //        HttpStatus = HttpStatusCode.ServiceUnavailable,
                        //        Message = exp!.Message,
                        //        BusData = exp!.Sql

                        //    };
                        //};
                    });
                    return sqlSugar;
                });
            }
            return services;
        }


        #endregion

        #region Swagger
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            #region 添加swagger注释
            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1", new OpenApiInfo
                //{
                //    Version = "v1",
                //    Title = "OpenHIS开源综合Api"
                //});
                c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{AppDomain.CurrentDomain.FriendlyName}.xml"), true);//WebAPI项目XML文件
                //c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SugarProjectCore.xml"), true);//其他项目所需的XML文件

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Value: Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                  {
                    new OpenApiSecurityScheme
                    {
                      Reference = new OpenApiReference
                      {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                      },Scheme = "oauth2",Name = "Bearer",In = ParameterLocation.Header,
                    },new List<string>()
                  }
                });
            });
            return services;
            #endregion
        }
        #endregion


        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddLibLogging(this IServiceCollection services)
        {
            services.AddLogging(options =>
            {
                options.AddLog4Net("Configs/Log4Net.config");
                options.AddConsole();
            });
            return services;
        }



        /// <summary>
        /// Web 通用自定义组件
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebServiceBase(this IServiceCollection services, IConfiguration config)
        {
            ConfigServiceBase(services, config);
            services.AddDependencyService();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //HttpContext 访问组件
            HttpWebHelper.servicesCollection = services;
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = config.GetConnectionString("RedisConfig:Connection");
                options.InstanceName = "OpenhisInstance";
            });

            return services;
        }

        /// <summary>
        /// 接口 通用自定义组件
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddCorsService(this IServiceCollection services)
        {
            //配置跨域服务
            services.AddCors(options =>
            {
                options.AddPolicy("Cors", p =>
                {
                    p.WithOrigins(ConfigInitHelper.SysConfig.CorsWithOrigins!.Split(',')).AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                });
            });
            return services;
        }
        public static IServiceCollection AddInterfaceServiceBase(this IServiceCollection services, IConfiguration config)
        {
            ConfigServiceBase(services, config);

            services.AddLibLogging();
            services.AddHttpContextAccessor();
            services.AddDependencyService();
            return services;
        }



        #region private
        private static IServiceCollection ConfigServiceBase(this IServiceCollection services, IConfiguration config)
        {
            #region 全局配置
            ConfigInitHelper.DbConfig = config.GetSection("BusinessDB").Get<BusinessDB>();
            ConfigInitHelper.SysConfig = config.GetSection("BusinessConfig").Get<BusinessConfig>();
            ConfigInitHelper.AccessSetting = config.GetSection("BusinessConfig").Get<AccessConfig>();
            services.AddSingleton(new RedisHelper(config.GetSection("RedisConfig")));
            services.AddSingleton(new AppSettings(config));
            #endregion
            return services;
        }
        #endregion

        #region 暂停使用
        public static IServiceCollection AddSqlsugarBaseDB(this IServiceCollection services, IConfiguration configuration)
        {
            if (string.IsNullOrWhiteSpace(ConfigInitHelper.DbConfig!.DefaultConn) && ConfigInitHelper.DbConfig.DBConf.Count > 0)
            {
                return services;
            }
            var dbMain = ConfigInitHelper.DbConfig.DBConf.FirstOrDefault();
            //注册SqlSugar
            services.AddSingleton<ISqlSugarClient>(s =>
            {
                SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
                {
                    DbType = SqlSugar.DbType.SqlServer,
                    ConnectionString = string.Format(ConfigInitHelper.DbConfig.DefaultConn ?? "", dbMain!.DBName),
                    IsAutoCloseConnection = true,
                    ConfigId = dbMain!.ConnId ?? "1"
                },
               db =>
               {
                   //单例参数配置，所有上下文生效
                   db.Aop.OnLogExecuting = (sql, pars) =>
                   {
                       //获取作IOC作用域对象
                       var appServive = s.GetService<IHttpContextAccessor>();
                       var obj = appServive?.HttpContext?.RequestServices.GetService<ILogger>();
                       Console.WriteLine("AOP" + obj!.GetHashCode());
                   };
               });
                return sqlSugar;
            });
            //业务库是动态添加的，只要注入主库就行
            return services;
        }

        #region IOC
        /// <summary>
        /// 服务层注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyString">程序集名称（反射加载程序集使用）</param>
        /// <param name="endsWithName">规范化服务层类结尾名称，默认以Service结尾，忽略大小写</param>
        /// <returns></returns>
        public static IServiceCollection ServiceInjection(this IServiceCollection services, string assemblyString = "", string endsWithName = "")
        {
            if (string.IsNullOrWhiteSpace(assemblyString) || string.IsNullOrWhiteSpace(endsWithName))
            {
                var dic = services.BuildServiceProvider().GetService<IOptions<DependencyInjectionConfigure>>();
                if (dic != null && dic.Value != null)
                {
                    assemblyString = string.IsNullOrWhiteSpace(assemblyString) ? dic.Value.ServiceAssemblyName : assemblyString;
                    endsWithName = string.IsNullOrWhiteSpace(endsWithName) ? dic.Value.ServiceEndsWith : endsWithName;
                }
                endsWithName = string.IsNullOrWhiteSpace(endsWithName) ? "Service" : endsWithName;
            }
            if (string.IsNullOrWhiteSpace(assemblyString))
            {
                return services;
            }
            try
            {
                var serviceAssembly = Assembly.Load(assemblyString);
                var serviceList = serviceAssembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType && t.Name.EndsWith(endsWithName, StringComparison.OrdinalIgnoreCase)).ToList();
                foreach (var type in serviceList)
                {
                    var interFaceList = type.GetInterfaces();
                    if (interFaceList.Any())
                    {
                        foreach (var i in interFaceList)
                        {
                            services.AddScoped(i, type);
                        }
                        //services.AddScoped(interFaceList.Where(p => p.Name.EndsWith(endsWithName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault(), type);
                    }
                }
            }
            catch (Exception)
            {

            }
            return services;
        }

        /// <summary>
        /// 仓储层注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyString">程序集名称（反射加载程序集使用）</param>
        /// <param name="endsWithName">规范化仓库层类结尾名称，默认以Repository结尾，忽略大小写</param>
        /// <returns></returns>
        public static IServiceCollection RepositoryInjection(this IServiceCollection services, string assemblyString = "", string endsWithName = "")
        {
            if (string.IsNullOrWhiteSpace(assemblyString) || string.IsNullOrWhiteSpace(endsWithName))
            {
                var dic = services.BuildServiceProvider().GetService<IOptions<DependencyInjectionConfigure>>();
                if (dic != null && dic.Value != null)
                {
                    assemblyString = string.IsNullOrWhiteSpace(assemblyString) ? dic.Value.RepositoryAssemblyName : assemblyString;
                    endsWithName = string.IsNullOrWhiteSpace(endsWithName) ? dic.Value.RepositoryEndsWith : endsWithName;
                }
                endsWithName = string.IsNullOrWhiteSpace(endsWithName) ? "Repository" : endsWithName;
            }
            if (string.IsNullOrWhiteSpace(assemblyString))
            {
                return services;
            }

            var serviceAssembly = Assembly.Load(assemblyString);
            var repositoryList = serviceAssembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType && t.Name.EndsWith(endsWithName, StringComparison.OrdinalIgnoreCase)).ToList();
            foreach (var type in repositoryList)
            {
                var interFaceList = type.GetInterfaces();
                if (interFaceList.Any())
                {
                    services.AddScoped(interFaceList.FirstOrDefault(p => p.Name.EndsWith(endsWithName, StringComparison.OrdinalIgnoreCase)), type);
                }
            }
            return services;
        }

        #endregion
        #endregion
    }
}