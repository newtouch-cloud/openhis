using System.Runtime.CompilerServices;

namespace NewtouchHIS.Lib.Base.Model
{
    public class Request<T> : RequestBase
    {
        public Request() { }
        /// <summary>
        /// 业务请求数据
        /// </summary>
        public T? Data { get; set; }


    }

    public class RequestBus<T> : BusRequest
    {
        /// <summary>
        /// 业务请求数据
        /// </summary>
        public T? Data { get; set; }
    }

    /// <summary>
    /// 自动封装Request
    /// </summary>
    public static class RequestApiGen
    {

        public static Request<T> GetRequest<T>(this Request<T> request)
        {
            request.AppId = request.AppId ?? ConfigInitHelper.SysConfig?.AppId;
            request.Timestamp = DateTime.Now;
            return request;
        }

    }
}
