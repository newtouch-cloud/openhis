using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using Autofac;
using Newtonsoft.Json;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Utils;
using Newtouch.Core.Redis;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.API.Common.Attributes;
using Newtouch.HIS.API.Common.Exceptions;
using Newtouch.HIS.API.Common.Interface;
using Newtouch.HIS.API.Common.Models;
using Newtouch.Tools;
using NLog.Contact.DTO;

namespace FrameworkBase.API
{
    /// <summary>
    /// API Controll 基类
    /// </summary>
    public class ApiControllerBase<TControllerType> : ApiController
    {
        /// <summary>
        /// 是否为private readonly I 解析实例
        /// </summary>
        private static bool? _PRFAutoResolve = null;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static ApiControllerBase()
        {
            _PRFAutoResolve = ConfigurationHelper.GetAppConfigBoolValue("PRFAutoResolve_WebAPI") ?? ConfigurationHelper.GetAppConfigBoolValue("PRFAutoResolve");
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ApiControllerBase(IComponentContext com)
        {
            //当前Controller内部可用
            _componentContext = com;
            //其他地方用
            HttpContext.Current.Items["API_IComponentContext"] = com;

            if (_PRFAutoResolve ?? true)
            {
                //利用IOC为private readonly field赋值
                this.PRFAutoResolve();
            }

            this._userIdentityResolver = com.Resolve<IUserIdentityResolver<Identity>>();
            this._enterTime = DateTime.Now;
        }

        /// <summary>
        /// IOC解析器
        /// </summary>
        private readonly IComponentContext _componentContext;
        //用户Identity解析器（从token中解析身份）
        private readonly IUserIdentityResolver<Identity> _userIdentityResolver;
        //访问enter时间
        private readonly DateTime? _enterTime;

        /// <summary>
        /// 用户身份
        /// </summary>
        protected internal Identity Identity { get; private set; }

        /// <summary>
        /// 处理（内部异常捕获）
        /// </summary>
        /// <typeparam name="TRequestModel"></typeparam>
        /// <typeparam name="TResponseModel"></typeparam>
        /// <param name="ac"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [NonAction]
        protected internal ResponseBase CommonExecute<TRequestModel, TResponseModel>(Action<TRequestModel, TResponseModel> ac
            , TRequestModel request)
            where TRequestModel : RequestBase
            where TResponseModel : ResponseBase
        {
            var enterTime = DateTime.Now;
            var controllerName = ActionContext.ControllerContext.ControllerDescriptor.ControllerName;
            var actionName = ActionContext.ActionDescriptor.ActionName;

            var response = Activator.CreateInstance<TResponseModel>();
            try
            {
                if (request == null)
                {
                    response.code = ResponseResultCode.FAIL;
                    response.sub_code = "SOMEARGUMENTS_IS_REQUIRED";
                }
                else if (request.Timestamp == null)
                {
                    response.code = ResponseResultCode.FAIL;
                    response.sub_code = "TIMESTAMP_IS_REQUIRED";
                }
                else
                {
                    ////2017-03-27调整了Authorize和Validate的顺序，因为可以从token中提取request需要的相关身份参数
                    Authorize(request.Token, request.TokenType);
                    if (Identity != null)
                    {
                        Ext.MapperTo(this.Identity, request);
                    }
                    request.Validate();

                    ac(request, response);
                }
            }
            catch (TokenTypeRequiredException)
            {
                response.code = ResponseResultCode.FAIL;
                response.sub_code = "TOKEN_TYPE_IS_REQUIRED";
                response.sub_msg = "访问令牌类型为空";
            }
            catch (TokenRequiredException)
            {
                response.code = ResponseResultCode.FAIL;
                response.sub_code = "TOKEN_IS_REQUIRED";
                response.sub_msg = "访问令牌为空";
            }
            catch (TokenExpiredException)
            {
                response.code = ResponseResultCode.FAIL;
                response.sub_code = "TOKEN_IS_EXPIRED";
                response.sub_msg = "访问令牌已过期";
            }
            catch (TokenTypeNotMatchException)
            {
                response.code = ResponseResultCode.FAIL;
                response.sub_code = "TOKEN_TYPE_NOT_MATCH";
                response.sub_msg = "访问令牌类型不匹配";
            }
            catch (TokenIllegalException)
            {
                response.code = ResponseResultCode.FAIL;
                response.sub_code = "TOKEN_IS_ILLEGAL";
                response.sub_msg = "接口访问权限不足";
            }
            catch (ArgumentsRequiredException ex)
            {
                response.code = ResponseResultCode.FAIL;
                response.sub_code = string.Format("{0}_IS_REQUIRED"
                    , !string.IsNullOrWhiteSpace(ex.Fields) ? ex.Fields.ToUpper() : "SOMEARGUMENTS");
            }
            catch (FailedException ex)
            {
                response.code = ResponseResultCode.FAIL;
                response.sub_code = (ex.Code ?? "").ToUpper();
                response.sub_msg = ex.Msg;
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                if (AppLogger.Instance != null)
                {
                    AppLogger.Instance.Error(string.Format("执行{0}过程中发生异常", actionName), ex);
                }

                response.code = ResponseResultCode.ERROR;
                response.sub_code = "INTERNAL_SERVER_ERROR";
                response.sub_msg = ex.Message + ex.StackTrace;
            }

            //记录访问日志
            if (AppLogger.Instance != null)
            {
                var requestStr = JsonConvert.SerializeObject(request);
                var m = new MoniterDTO
                {
                    AreaName = "api",
                    ControllerName = controllerName,
                    ActionName = actionName,
                    EnterTime = enterTime,
                    CostMSec = (int)(DateTime.Now - enterTime).TotalMilliseconds,
                    RequestForm = new List<CustomDictionary> { new CustomDictionary { Key = "RequestFormKey", Value = requestStr } },
                    ResponseStatusCode = (int)response.code,
                    ResponseSubCode = response.sub_code,
                    ResponseSubMsg = response.sub_msg,
                    UserIdentity = this.Identity == null ? null : this.Identity.Account,
                    ResponseContent = response.ToJson(),
                };
                AppLogger.Instance.Moniter(m);
            }
            if (ApiControllerBaseEx._dbContextDisposer != null)
            {
                ApiControllerBaseEx._dbContextDisposer();
            }
            return response;
        }

        /// <summary>
        /// 返回HttpResponseMessage
        /// </summary>
        /// <typeparam name="TRequestModel"></typeparam>
        /// <typeparam name="TResponseModel"></typeparam>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        [NonAction]
        protected internal HttpResponseMessage CreateResponse<TRequestModel, TResponseModel>(TRequestModel request, TResponseModel response)
                        where TRequestModel : JSONRequestBase
            where TResponseModel : ResponseBase
        {
            if (request != null && !string.IsNullOrWhiteSpace(request.jsonpCallback))
            {
                //jsonp请求
                return CreateJsonPResponse(response, request.jsonpCallback);
            }
            return CreateResponse(response);
        }

        /// <summary>
        /// 返回HttpResponseMessage
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        [NonAction]
        protected internal HttpResponseMessage CreateResponse(object obj, string mediaType = "application/json")
        {
            var content = GetObjectJsonStr(obj);
            return CreateResponse(content, mediaType);
        }

        /// <summary>
        /// 返回HttpResponseMessage
        /// </summary>
        /// <param name="content"></param>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        [NonAction]
        protected internal HttpResponseMessage CreateResponse(string content, string mediaType = "application/json")
        {
            return new HttpResponseMessage { Content = new StringContent(content, Encoding.GetEncoding("UTF-8"), mediaType) };
        }

        /// <summary>
        /// 返回HttpResponseMessage（jsonp 构造返回结果）
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        [NonAction]
        protected internal HttpResponseMessage CreateJsonPResponse(object obj, string callback)
        {
            string str;
            if (obj is string || obj is char)
            {
                str = obj.ToString();
            }
            else
            {
                str = GetObjectJsonStr(obj);
                str = string.Format("{0}({1})", callback, str);
            }
            var result = new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };
            result.Headers.Add("Access-Control-Allow-Origin", "*");
            return result;
        }

        /// <summary>
        /// 对象列集合过滤（接口只需返回其中几列）
        /// 示例：在Action：resp.data = base.FilterObjectProperty(list, req.ResponseColumns, includeProps: new string[] { "c1", "c2" });
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="responseParams"></param>
        /// <param name="includeProps"></param>
        /// <param name="excludeProps"></param>
        /// <returns></returns>
        [NonAction]
        protected internal object FilterObjectProperty(object obj, string responseParams = null, string[] includeProps = null, string[] excludeProps = null)
        {
            if (!string.IsNullOrWhiteSpace(responseParams))
            {
                //只需要其中几列
                var propNames = ArrayConvertor.Convert(responseParams, ',');
                if (propNames != null && propNames.Length > 0)
                {
                    //给
                    includeProps = includeProps != null ? includeProps.Union(propNames).Distinct().ToArray() : propNames;
                }
            }
            var str = GetObjectJsonStr(obj, includeProps, excludeProps);
            return JsonConvert.DeserializeObject(str);
        }

        /// <summary>
        ///  json序列化（指定列集合）
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="retainProps"></param>
        /// <param name="excludeProps"></param>
        /// <returns></returns>
        [NonAction]
        protected internal string GetObjectJsonStr(object obj, string[] retainProps = null, string[] excludeProps = null)
        {
            var jsetting = new JsonSerializerSettings();
            if ((retainProps != null && retainProps.Length > 0)
                || (excludeProps != null && excludeProps.Length > 0))
            {
                jsetting.ContractResolver = new LimitPropsContractResolver(retainProps, excludeProps);
            }
            jsetting.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(obj, Formatting.Indented, jsetting);
        }

        #region private methods

        /// <summary>
        /// 身份验证
        /// </summary>
        /// <param name="token"></param>
        /// <param name="tokenType"></param>
        public virtual void Authorize(string token, string tokenType)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                this.Identity = _userIdentityResolver.GetIdentity(token, tokenType);
                if (this.Identity == null || string.IsNullOrWhiteSpace(this.Identity.Account) || string.IsNullOrWhiteSpace(this.Identity.UserId))
                {
                    throw new TokenExpiredException(); //身份认证失败
                }
            }
            if (this.Identity == null)
            {
                bool needAuthorize = false; //该接口是否需要身份认证
                var controllerType = ControllerContext.ControllerDescriptor.ControllerType;
                var actionName = ActionContext.ActionDescriptor.ActionName;

                if (controllerType.GetCustomAttributes(false).Where(p => p is DefaultAuthorizeAttribute).Any())
                {
                    needAuthorize = true;
                }
                else
                {
                    var method = controllerType.GetMethod(actionName);
                    if (method != null)
                    {
                        if (method.GetCustomAttributes(false).Where(p => p is DefaultAuthorizeAttribute).Any())
                        {
                            needAuthorize = true;
                        }
                    }
                }

                if (needAuthorize)
                {
                    if (string.IsNullOrWhiteSpace(token))
                    {
                        throw new TokenRequiredException();
                    }
                    else
                    {
                        throw new TokenExpiredException();
                    }

                }
            }
            if (this.Identity != null)
            {
                HttpContext.Current.Items["API_UserIdentity_Account"] = this.Identity.Account;
            }
        }

        #endregion

        /// <summary>
        /// 保存用户身份，并返回token
        /// （缓存在Redis）
        /// </summary>
        /// <param name="iden"></param>
        /// <returns></returns>
        [NonAction]
        protected internal string SaveIdentity(Identity iden)
        {
            return ApiControllerBaseEx.SaveIdentity(iden);
        }

    }

    /// <summary>
    /// Ex
    /// </summary>
    public static class ApiControllerBaseEx
    {
        /// <summary>
        /// 委托：释放数据库链接（在每一次API请求 处理之后、返回之前调用）
        /// </summary>
        public static Action _dbContextDisposer { private set; get; }

        /// <summary>
        /// 注册 释放数据库链接的方法
        /// </summary>
        /// <param name="dbContextDisposer"></param>
        public static void RegisterDBContextDisposer(Action dbContextDisposer)
        {
            _dbContextDisposer = dbContextDisposer;
        }

        /// <summary>
        /// Retrieve a service
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <returns></returns>
        public static object ResolveService(Type serviceType)
        {
            var com = HttpContext.Current.Items["API_IComponentContext"] as IComponentContext;
            return com != null ? com.Resolve(serviceType) : null;
        }

        /// <summary>
        /// 保存用户身份，并返回token
        /// （依赖Redis）
        /// </summary>
        /// <param name="iden"></param>
        /// <returns></returns>
        [NonAction]
        public static string SaveIdentity(Identity iden)
        {
            var token = Guid.NewGuid().ToString();
            RedisHelper.StringSet(token, JsonConvert.SerializeObject(iden), new TimeSpan(0, 20, 0));
            return token;
        }

        /// <summary>
        /// 根据token，获取用户身份
        /// （缓存在Redis）
        /// </summary>
        /// <typeparam name="TIdentity"></typeparam>
        /// <param name="token"></param>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        public static TIdentity GetIdentity<TIdentity>(string token, string tokenType = null) where TIdentity : Identity
        {
            var tokenVal = RedisHelper.Get<string>(token);
            if (string.IsNullOrWhiteSpace(tokenVal)) return null;
            var identity = JsonConvert.DeserializeObject<TIdentity>(tokenVal);
            RedisHelper.KeyExpire(token, new TimeSpan(0, 20, 0));   //重新设置token过期时间
            return identity;
        }

    }

}

