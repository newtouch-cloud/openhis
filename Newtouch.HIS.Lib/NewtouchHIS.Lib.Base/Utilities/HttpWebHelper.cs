using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NewtouchHIS.Lib.Base.Extension;
using System.Text;

namespace NewtouchHIS.Lib.Base.Utilities
{
    public class HttpWebHelper
    {
        public static IServiceCollection? servicesCollection;
        public static HttpContext HttpCurrent
        {
            get
            {
                object factory = servicesCollection.BuildServiceProvider().GetService(typeof(IHttpContextAccessor));
                HttpContext context = ((IHttpContextAccessor)factory).HttpContext;
                return context;
            }
        }

        #region Session操作

        /// <summary>
        /// 写Session
        /// </summary>
        /// <param name="key">Session的键名</param>
        /// <param name="value">Session的键值</param>
        public static void WriteSession(string key, string value)
        {
            WriteSession<string>(key, value);
        }

        /// <summary>
        /// 写Session
        /// </summary>
        /// <typeparam name="T">Session键值的类型</typeparam>
        /// <param name="key">Session的键名</param>
        /// <param name="value">Session的键值</param>
        public static void WriteSession<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key) || value == null)
                return;
            HttpCurrent.Session.SetString(key, value.ToJson());
        }

        /// <summary>
        /// 读取Session的值
        /// </summary>
        /// <param name="key">Session的键名</param>        
        public static string GetSession(string key)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;
            return HttpCurrent.Session.GetString(key);
        }

        /// <summary>
        /// 获取Session中的对象值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetSessionObjVal<T>(string key)
            where T : class
        {
            if (string.IsNullOrEmpty(key))
                return null;
            var val = HttpCurrent.Session.GetString(key);
            if (!string.IsNullOrEmpty(val))
            {
                return JsonConvert.DeserializeObject<T>(val);
            }
            return null;
        }

        /// <summary>
        /// 删除指定Session
        /// </summary>
        /// <param name="key">Session的键名</param>
        public static void RemoveSession(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;
            HttpCurrent.Session.Remove(key);
        }

        #endregion

        #region Cookie操作

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            CookieOptions options = new CookieOptions();

            var cookie = HttpCurrent.Request.Cookies[strName];
            if (cookie == null)
            {
                HttpCurrent.Response.Cookies.Append(strName, strValue);
            }
            else
            {

            }

        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            RemoveCookie(strName);
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddMinutes(expires);//过期时间
            HttpCurrent.Response.Cookies.Append(strName, strValue, options);
        }
        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string? GetCookie(string strName)
        {
            return HttpCurrent.Request.Cookies[strName];
        }

        /// <summary>
        /// 删除Cookie对象
        /// </summary>
        /// <param name="CookiesName">Cookie对象名称</param>
        public static void RemoveCookie(string CookiesName)
        {
            HttpCurrent.Response.Cookies.Delete(CookiesName);
        }

        #endregion

        /// <summary>
        /// 获取完整请求Url
        /// </summary>
        /// <returns></returns>
        public static string GetCompleteUrl()
        {
            if (HttpCurrent != null && HttpCurrent.Request != null)
            {
                return new StringBuilder()
               .Append(HttpCurrent.Request.Scheme)
               .Append("://")
               .Append(HttpCurrent.Request.Host)
               .Append(HttpCurrent.Request.PathBase)
               .Append(HttpCurrent.Request.Path)
               .Append(HttpCurrent.Request.QueryString)
               .ToString();
            }
            return string.Empty;
        }

    }
}
