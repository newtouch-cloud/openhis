using Newtonsoft.Json;
using System.Net;

namespace NewtouchHIS.Lib.Base.Model
{
    public class BusResult<T>
    {
        /// <summary>
        /// 业务返回码
        /// </summary>
        public ResponseResultCode code { get; set; }

        /// <summary>
        /// 业务返回码描述
        /// </summary>
        public string? msg { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        [JsonProperty("data")]
        public T? Data { get; set; }

        public BusResult()
        {
            code = ResponseResultCode.Default;
            msg = "";
        }
        public BusResult(bool isSuccess = false, string? msg = null)
        {
            code = isSuccess ? ResponseResultCode.SUCCESS : ResponseResultCode.FAIL;
            msg = msg ?? "";
        }
    }

    public class BusResultPage<T> : BusResult<T>
    {
        public Pagination page { get; set; }
        public BusResultPage()
        {
            page = new Pagination() { page = 1, rows = 20 };
        }
    }

    /// <summary>
    /// 数据返回模型基类
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        public virtual HttpStatusCode HttpStatus { get; set; } = HttpStatusCode.OK;

        /// <summary>
        /// 获取 消息内容
        /// </summary>
        public virtual string? Message { get; set; } = "OK";

        /// <summary>
        /// 返回数据
        /// </summary>
        public virtual object? BusData { get; set; }

        /// <summary>
        /// 执行时长
        /// </summary>
        public virtual long TimeOut { get; set; }

        public static Result BuildResult(object? data, string? errormsg, long elapsedMilliseconds, ResponseResultCode responseResultCode, int httpStatusCode = 200)
        {
            return new Result
            {
                HttpStatus = (HttpStatusCode)httpStatusCode,
                BusData = new ResponseBase
                {
                    code = responseResultCode,
                    msg = errormsg,
                    data = data
                },
                TimeOut = elapsedMilliseconds
            };
        }

        public static Result ResultException(string msg, object? data = null, long elapsedMilliseconds = 0)
        {
            return new Result
            {
                HttpStatus = HttpStatusCode.OK,
                BusData = new ResponseBase
                {
                    code = ResponseResultCode.FAIL,
                    msg = msg,
                    data = data
                },
                TimeOut = elapsedMilliseconds
            };
        }


    }

    public class ApiManageResult<T>
    {
        public int HttpStatus { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// 执行时长
        /// </summary>
        public virtual long TimeOut { get; set; }
        public BusResult<T> BusData { get; set; }
    }

}
