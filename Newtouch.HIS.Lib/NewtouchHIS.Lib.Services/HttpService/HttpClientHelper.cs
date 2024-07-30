using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NewtouchHIS.Lib.Base.Extension;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;

namespace NewtouchHIS.Lib.Services.HttpService
{
    public class HttpClientHelper : IHttpClientHelper
    {
        readonly ILogger<HttpClientHelper> _logger;

        public HttpClientHelper(ILogger<HttpClientHelper> logger)
        {
            _logger = logger;
        }
        #region async
        public async Task<T> PostAsync<T>(string url, string data)
        {
            var result = default(T);
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 0, 600); // 10是秒数，用于设置超时时长
                HttpContent content = new StringContent(data);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Connection.Add("keep-alive");
                var res = await client.PostAsync(url, content);
                if (res.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<T>(res.Content.ReadAsStringAsync().Result);
                }
                client.Dispose();
            }
            return result;
        }
        public async Task<T> PostAsync<T>(string url, string data,string token)
        {
            var result = default(T);
            using (HttpClient client = new HttpClient())
            {
                // 构造 HTTP 请求消息
                var requestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(url),
                    Content = new StringContent(data),
                    
                };
                client.Timeout = new TimeSpan(0, 0, 600); // 10是秒数，用于设置超时时长
                client.DefaultRequestHeaders.Connection.Add("keep-alive");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer",token);
                requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                 
                var res = await client.SendAsync(requestMessage);
                if (res.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<T>(res.Content.ReadAsStringAsync().Result);
                }
                client.Dispose();
            }
            return result;
        }
        /// <summary>
        /// 支持Https请求 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="header"></param>
        /// <param name="requestEncoding"></param>
        /// <param name="mediaType"></param>
        /// <param name="CertificateCustomValid">访问的 https 网站是使用自签名证书或不受信任的证书，需要使用以下代码禁用 SSL/TLS 校验</param>
        /// <returns></returns>
        public async Task<T> PostAsync<T>(string url, string data, Dictionary<string, string> header, Encoding? requestEncoding = null, string mediaType = "application/json", bool CertificateCustomValid = false)
        {
            var result = default(T);
            using (HttpClient client = CertificateCustomValid ? new HttpClient()
                : new HttpClient(new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                }))
            {
                if (url.StartsWith("https://"))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                }
                client.Timeout = new TimeSpan(0, 0, 600); // 10是秒数，用于设置超时时长
                HttpContent content = new StringContent(data);
                if (header != null && header.Count() > 0)
                {
                    foreach (var item in header)
                    {
                        content.Headers.Add(item.Key, item.Value);
                    }
                }
                content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
                client.DefaultRequestHeaders.Connection.Add("keep-alive");
                var res = await client.PostAsync(url, content);
                if (res.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<T>(res.Content.ReadAsStringAsync().Result);
                }
                client.Dispose();
            }
            return result;
        }
        #endregion

        #region get
        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string Get(string url, out string statusCode, Dictionary<string, string>? header = null)
        {
            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(
              new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync(url).Result;
            statusCode = response.StatusCode.ToString();
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            return null;
        }


        public async Task<T> GetAsync<T>(string url, Dictionary<string, string>? header = null)
        {
            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            if (header != null && header.Count() > 0)
            {
                foreach (var item in header)
                {
                    httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
            HttpResponseMessage response = await httpClient.GetAsync(url);

            T result = default(T);

            if (response.IsSuccessStatusCode)
            {
                Task<string> t = response.Content.ReadAsStringAsync();
                string s = t.Result;

                result = JsonConvert.DeserializeObject<T>(s);
            }
            return result;
        }
        #endregion

        #region Newtouch
        /// <summary>
        /// 普通Post方法，提交json数据，返回泛型示例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="postDataStr"></param>
        /// <param name="timeout"></param>
        /// <param name="userAgent"></param>
        /// <param name="requestEncoding"></param>
        /// <param name="responseEncoding"></param>
        /// <param name="cookies"></param>
        /// <param name="contentType">默认json</param>
        /// <returns></returns>
        public T HttpPostStringAndRead<T>(string url, string postDataStr
            , int? timeout = 120000, string? userAgent = null, Encoding? requestEncoding = null
            , Encoding? responseEncoding = null
            , CookieCollection? cookies = null
            , EnumContentType? contentType =
            EnumContentType.json)
        {
            var str = HttpPostString(url, postDataStr, timeout, userAgent, requestEncoding, responseEncoding
                , cookies, contentType);
            return str.ToObject<T>();
        }

        /// <summary>
        /// Http请求的默认代理：’Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)‘
        /// </summary>
        public static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        /// <summary>
        /// 请求文档类型
        /// </summary>
        public enum EnumContentType
        {
            /// <summary>
            /// json
            /// </summary>
            json,
            /// <summary>
            /// form
            /// </summary>
            form,
            /// <summary>
            /// formdata
            /// </summary>
            formdata,
            /// <summary>
            /// xml
            /// </summary>
            xml
        }

        #region GET

        /// <summary>
        /// 普通Get方法，返回string（out参数http状态码）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="responseStatusCode">HTTP状态码</param>
        /// <param name="timeout">请求超时 时间（以毫秒为单位）</param>
        /// <param name="userAgent"></param>
        /// <param name="responseEncoding">响应编码，未指定时默认UTF8</param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static string? HttpGetString(string url, out int responseStatusCode
            , int? timeout = 120000, string? userAgent = null
            , Encoding? responseEncoding = null, CookieCollection? cookies = null)
        {
            try
            {
                HttpWebResponse response;
                var resp = HttpGetString(url, out response, timeout: timeout, userAgent: userAgent, responseEncoding: responseEncoding, cookies: cookies);

                responseStatusCode = (int)response.StatusCode;

                return resp;
            }
            catch (WebException wex)
            {
                responseStatusCode = (int)((((HttpWebResponse)wex.Response)).StatusCode);
                return string.Empty;
            }
        }

        /// <summary>
        /// 普通Get方法，返回string
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeout">请求超时 时间（以毫秒为单位）</param>
        /// <param name="userAgent"></param>
        /// <param name="responseEncoding">响应编码，未指定时默认UTF8</param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static string HttpGetString(string url
            , int? timeout = 120000, string? userAgent = null
            , Encoding? responseEncoding = null, CookieCollection? cookies = null)
        {
            HttpWebResponse response;
            var resp = HttpGetString(url, out response, timeout: timeout, userAgent: userAgent, responseEncoding: responseEncoding, cookies: cookies);
            return resp;
        }

        #endregion GET end

        #region POST

        /// <summary>
        /// 普通Post方法，提交json数据，返回String（out参数http状态码）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postDataStr"></param>
        /// <param name="responseStatusCode">HTTP状态码</param>
        /// <param name="timeout">请求超时 时间（以毫秒为单位）</param>
        /// <param name="userAgent"></param>
        /// <param name="requestEncoding">请求内容编码，未指定时默认UTF8</param>
        /// <param name="responseEncoding">响应编码，未指定时默认UTF8</param>
        /// <param name="cookies"></param>
        /// <param name="contentType">默认form表单式提交</param>
        /// <returns></returns>
        public string HttpPostString(string url, string postDataStr
            , out int responseStatusCode
            , int? timeout = 120000, string? userAgent = null, Encoding? requestEncoding = null
            , Encoding? responseEncoding = null
            , CookieCollection? cookies = null
            , EnumContentType? contentType = null)
        {
            try
            {
                HttpWebResponse response;
                var resp = HttpPostString(url, postDataStr, out response, timeout: timeout
                    , userAgent: userAgent
                    , requestEncoding: requestEncoding
                    , responseEncoding: responseEncoding, cookies: cookies
                    , contentType: contentType);

                responseStatusCode = (int)response.StatusCode;

                return resp;
            }
            catch (WebException wex)
            {
                responseStatusCode = (int)((((HttpWebResponse)wex.Response)).StatusCode);
                return string.Empty;
            }
        }

        /// <summary>
        /// 普通Post方法，提交json数据，返回String
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postDataStr"></param>
        /// <param name="timeout">请求超时 时间（以毫秒为单位）</param>
        /// <param name="userAgent"></param>
        /// <param name="requestEncoding">请求内容编码，未指定时默认UTF8</param>
        /// <param name="responseEncoding">响应编码，未指定时默认UTF8</param>
        /// <param name="cookies"></param>
        /// <param name="contentType">默认form表单式提交</param>
        /// <returns></returns>
        public string HttpPostString(string url, string postDataStr
            , int? timeout = 120000, string? userAgent = null, Encoding? requestEncoding = null
            , Encoding? responseEncoding = null
            , CookieCollection? cookies = null
            , EnumContentType? contentType = null)
        {
            HttpWebResponse response;
            var resp = HttpPostString(url, postDataStr, out response, timeout: timeout
                , userAgent: userAgent
                , requestEncoding: requestEncoding
                , responseEncoding: responseEncoding, cookies: cookies
                , contentType: contentType);
            return resp;
        }

        #endregion POST end

        #region private mthods

        /// <summary>
        /// 普通Get方法，返回string
        /// </summary>
        /// <param name="url"></param>
        /// <param name="response">HttpWebResponse</param>
        /// <param name="timeout">请求超时 时间（以毫秒为单位）</param>
        /// <param name="userAgent"></param>
        /// <param name="responseEncoding">响应编码，未指定时默认UTF8</param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        private static string HttpGetString(string url
            , out HttpWebResponse response
            , int? timeout = 120000, string? userAgent = null
            , Encoding? responseEncoding = null, CookieCollection? cookies = null)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request!.Method = "GET";
            request.UserAgent = DefaultUserAgent;
            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }

            response = request.GetResponse() as HttpWebResponse;
            Stream myResponseStream = response!.GetResponseStream();

            if (responseEncoding == null)
            {
                responseEncoding = Encoding.UTF8;
            }
            StreamReader myStreamReader = new StreamReader(myResponseStream, responseEncoding);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        /// <summary>
        /// 普通Post方法，提交json数据，返回String
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postDataStr"></param>
        /// <param name="response">HttpWebResponse</param>
        /// <param name="timeout">请求超时 时间（以毫秒为单位）</param>
        /// <param name="userAgent"></param>
        /// <param name="requestEncoding">请求内容编码，未指定时默认UTF8</param>
        /// <param name="responseEncoding">响应编码，未指定时默认UTF8</param>
        /// <param name="cookies"></param>
        /// <param name="contentType">默认form表单式提交</param>
        /// <returns></returns>
        public string HttpPostString(string url, string postDataStr
            , out HttpWebResponse response
            , int? timeout = 120000, string? userAgent = null, Encoding? requestEncoding = null
            , Encoding? responseEncoding = null
            , CookieCollection? cookies = null
            , EnumContentType? contentType = null)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request!.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }

            request!.Method = "POST";
            request.ContentType = (contentType ?? EnumContentType.form) == EnumContentType.json
                ? "application/json"
                : (
                    (contentType ?? EnumContentType.form) == EnumContentType.xml
                    ? "text/xml"
                    : (
                        (contentType ?? EnumContentType.form) == EnumContentType.formdata
                        ? "multipart/form-data"
                        : "application/x-www-form-urlencoded"
                    )
                );

            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            else
            {
                request.UserAgent = DefaultUserAgent;
            }

            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }

            //如果需要POST数据  
            if (!string.IsNullOrWhiteSpace(postDataStr))
            {
                if (requestEncoding == null)
                {
                    requestEncoding = Encoding.UTF8;
                }
                byte[] data = requestEncoding.GetBytes(postDataStr.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }

            response = request.GetResponse() as HttpWebResponse;
            Stream myResponseStream = response!.GetResponseStream();
            if (responseEncoding == null)
            {
                responseEncoding = Encoding.UTF8;
            }
            StreamReader myStreamReader = new StreamReader(myResponseStream, responseEncoding);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        /// <summary>
        /// https
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }

        #endregion
        #endregion

    }


}
