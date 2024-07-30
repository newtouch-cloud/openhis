using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace NewtouchHIS.Lib.Services.HttpService
{
    public class HttpWebRequestHelper : IHttpWebRequestHelper
    {
        readonly ILogger<HttpWebRequestHelper> _logger;

        public HttpWebRequestHelper(ILogger<HttpWebRequestHelper> logger)
        {
            _logger = logger;
        }
        public T Request<T>(string url, string data, string method = "POST")
        {
            var result = default(T);
            HttpWebRequest request = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                var byteData = Encoding.UTF8.GetBytes(data);
                request.Method = method;
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                request.Timeout = 10000; // 超时时间，毫秒单位
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(byteData, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse(); // 发送请求
                using (var resStream = response.GetResponseStream()) // 读取数据
                {
                    using (var reader = new StreamReader(resStream, Encoding.UTF8))
                    {
                        result = JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"使用HttpWebRequest访问webapi方法异常：\r url:{url} \r data:{data} \r 异常信息:{ex.Message}");
            }
            finally
            {
                if (request != null)
                {
                    request.Abort(); // 释放资源
                }
            }
            return result;
        }
    }


}
