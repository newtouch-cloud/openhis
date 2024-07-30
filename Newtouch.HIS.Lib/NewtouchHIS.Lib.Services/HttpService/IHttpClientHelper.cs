using System.Net;
using System.Text;
using static NewtouchHIS.Lib.Services.HttpService.HttpClientHelper;

namespace NewtouchHIS.Lib.Services.HttpService
{
    public interface IHttpClientHelper
    {
        /// <summary>
        /// Post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<T> PostAsync<T>(string url, string data);
        /// <summary>
        /// Post请求（Header添加 Authorization）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<T> PostAsync<T>(string url, string data, string token);
        /// <summary>
        /// Post请求（支持Https）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="header"></param>
        /// <param name="requestEncoding"></param>
        /// <param name="mediaType"></param>
        /// <param name="CertificateCustomValid"></param>
        /// <returns></returns>
        Task<T> PostAsync<T>(string url, string data, Dictionary<string, string> header, Encoding? requestEncoding = null, string mediaType = "application/json", bool CertificateCustomValid = false);

        T HttpPostStringAndRead<T>(string url, string postDataStr
            , int? timeout = 120000, string? userAgent = null, Encoding? requestEncoding = null
            , Encoding? responseEncoding = null
            , CookieCollection? cookies = null
            , EnumContentType? contentType =
            EnumContentType.json);

        Task<T> GetAsync<T>(string url, Dictionary<string, string>? header = null);
    }


}
