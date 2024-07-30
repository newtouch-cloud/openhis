using Newtonsoft.Json;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Redis;
using Newtouch.Tools;
using Newtouch.Tools.Net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Infrastructure
{
    public class AuthenManageHelper
    {
        public readonly static string SiteTokenAPIHost = ConfigurationManager.AppSettings["SiteAuthCenterHost"];//token站点
        public readonly static string SiteConmonAPIHost = ConfigurationManager.AppSettings["SiteApiManageHost"]; //api中心站点
        public readonly static string appId = ConfigurationManager.AppSettings["AppId"];//api中心站点访问appid
        public readonly static string accessAppId = ConfigurationManager.AppSettings["Token_ApiManage"];//api中心站点访问appid
        //用户登陆缓存token
        public static string userCacheToken = "";

        /// <summary>
        /// 获取中心接口api token
        /// </summary>
        /// <returns></returns>
        public static string GetToken()
        {
            string tokenstr = RedisHelper.StringGet("AuthenGetTokenKey");
            string tokenstrs = "";
            if (!string.IsNullOrWhiteSpace(tokenstr))
            {
                tokenstrs = tokenstr;
            }
            else
            {
                var reqObj = new
                {
                    //AppId = appId,
                    AppId = appId,
                    Data = accessAppId
                };
                tokenstrs = HttpMethods.HttpPost(SiteTokenAPIHost + "api/Auth/GetAppToken", reqObj.ToJson(), null);
                AuthenTokenHelper Token = JsonConvert.DeserializeObject<AuthenTokenHelper>(tokenstrs);
                Tokendata data = Token.BusData.data.ToString().ToObject<Tokendata>();
                RedisHelper.StringSet("AuthenGetTokenKey", data.Token, new TimeSpan(0, 20, 0));
                tokenstrs = data.Token;
            }
            return tokenstrs;
        }

        public static string HttpPost(string data, string uri, string token)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                token = $"Bearer {token}";
            }
            return HttpMethods.HttpPostWithToken(uri, data, token);
        }
        public static BusData<T> HttpPost<T>(string data, string uri, OperatorModel user)
        {
            string token = "";
            try
            {
                token = GetUserToken(user);
                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new FailedException("[鉴权]未获取到 Api 身份令牌，请联系管理员");
                }
                token = $"Bearer {token}";
                string response = HttpMethods.HttpPostWithToken(uri, data, token);
                ApiManageResponse<T> apiResp = JsonConvert.DeserializeObject<ApiManageResponse<T>>(response);
                if (apiResp.HttpStatus == (int)HttpStatusCode.Unauthorized)
                {
                    //鉴权失败 刷新token
                    token = GetUserTokenRefesh(user, token);
                    //重新请求接口
                    response = HttpMethods.HttpPostWithToken(uri, data, token);
                    apiResp = JsonConvert.DeserializeObject<ApiManageResponse<T>>(response);
                }
                if (apiResp.HttpStatus != (int)HttpStatusCode.OK)
                {
                    throw new FailedException($"[Http]请求异常：{apiResp.HttpStatus}=>{apiResp.Message}");
                }
                return apiResp.BusData;
            }
            catch (Exception ex)
            {
                throw new FailedException($"[鉴权]获取到 Api 身份令牌异常：{ex.Message}");
            }
        }
        public static string GetUserTokenRefesh(OperatorModel user, string token)
        {
            var key = GetUserCacheKey(user.OrganizeId, user.UserCode);
            var reqObj = new
            {
                AppId = appId,
                OrganizeId = user.OrganizeId,
                Data = new
                {
                    TopOrganizeId = user.TopOrganizeId,
                    UserCode = user.UserCode,
                    UserName = user.UserName,
                    Account = user.UserCode,
                    Timestamp = DateTime.Now,
                    KeyLicense = accessAppId
                }
            };
            string tokenstrs = HttpMethods.HttpPostWithToken(SiteTokenAPIHost + "api/Auth/UserTokenRefresh", reqObj.ToJson(), token);
            var Token = JsonConvert.DeserializeObject<ApiManageResponse<Tokendata>>(tokenstrs);
            RedisHelper.StringSet(key, Token.BusData.data.Token, new TimeSpan(0, 20, 0));
            return Token.BusData.data.Token;
        }
        /// <summary>
        /// 用户登陆后获取api通用 token
        /// </summary>
        /// <returns></returns>
        public static string GetUserToken(OperatorModel user)
        {
            var key = GetUserCacheKey(user.OrganizeId, user.UserCode);
            string tokenstr = RedisHelper.StringGet(key);
            string tokenstrs = "";
            if (!string.IsNullOrWhiteSpace(tokenstr))
            {
                tokenstrs = tokenstr;
            }
            else
            {
                var reqObj = new
                {
                    AppId = appId,
                    OrganizeId = user.OrganizeId,
                    Data = new
                    {
                        TopOrganizeId = user.TopOrganizeId,
                        UserCode = user.UserCode,
                        UserName = user.UserName,
                        Account = user.UserCode,
                        Timestamp = DateTime.Now,
                        KeyLicense = accessAppId
                    }
                };
                tokenstrs = HttpMethods.HttpPost(SiteTokenAPIHost + "api/Auth/GetUserToken", reqObj.ToJson(), null);
                var Token = JsonConvert.DeserializeObject<ApiManageResponse<Tokendata>>(tokenstrs);
                RedisHelper.StringSet(key, Token.BusData.data.Token, new TimeSpan(0, 20, 0));
                tokenstrs = Token.BusData.data.Token;
            }
            return tokenstrs;
        }
        public static string GetUserCacheKey(string orgId, string user = null)
        {
            if (string.IsNullOrWhiteSpace(accessAppId))
            {
                throw new FailedException("[鉴权]未读取到接口AppId配置（Token_ApiManage）信息");
            }
            if (string.IsNullOrWhiteSpace(userCacheToken))
            {
                userCacheToken = string.Format(CacheKey.AccessToken, appId, orgId, accessAppId) + $"_{user}";
            }
            return userCacheToken;
        }
        ///// <summary>
        ///// 接口请求
        ///// </summary>
        ///// <param name="data"></param>
        ///// <param name="uri"></param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //public static string HttpPost(string data, string uri, string token)
        //{
        //    //先根据用户请求的uri构造请求地址
        //    string serviceUrl = string.Format(uri);
        //    //创建Web访问对象
        //    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
        //    //把用户传过来的数据转成“UTF-8”的字节流
        //    byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(data);

        //    myRequest.Method = "POST";
        //    myRequest.ContentLength = buf.Length;
        //    myRequest.ContentType = "application/json";
        //    myRequest.MaximumAutomaticRedirections = 1;
        //    myRequest.AllowAutoRedirect = true;
        //    if (!string.IsNullOrWhiteSpace(token))
        //    {
        //        myRequest.Headers.Add("Authorization", "Bearer " + token);
        //    }
        //    //发送请求
        //    Stream stream = myRequest.GetRequestStream();
        //    stream.Write(buf, 0, buf.Length);
        //    stream.Close();

        //    //获取接口返回值
        //    //通过Web访问对象获取响应内容
        //    HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
        //    //通过响应内容流创建StreamReader对象，因为StreamReader更高级更快
        //    StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
        //    //string returnXml = HttpUtility.UrlDecode(reader.ReadToEnd());//如果有编码问题就用这个方法
        //    string result = reader.ReadToEnd();//利用StreamReader就可以从响应内容从头读到尾
        //    reader.Close();
        //    myResponse.Close();
        //    return result;

        //}
    }
    public class ApiManageResponse<T>
    {
        public int HttpStatus { get; set; }
        public string Message { get; set; }
        public BusData<T> BusData { get; set; }
    }
    public class BusData<T>
    {
        public int code { get; set; }
        public string msg { get; set; }
        public T data { get; set; }
    }
    public class AuthenTokenHelper
    {
        public int HttpStatus { get; set; }
        public string Message { get; set; }
        public BusData BusData { get; set; }

    }
    public class BusData
    {
        public int code { get; set; }
        public string msg { get; set; }
        public object data { get; set; }
    }
    public class Tokendata
    {
        public string Token { get; set; }
    }

}
