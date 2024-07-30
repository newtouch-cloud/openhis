using NewtouchHIS.Lib.Base.Model;
using System.DirectoryServices.Protocols;
using System.Runtime.CompilerServices;

namespace NewtouchHIS.Lib.Base.Exceptions
{
    /// <summary>
    /// 业务处理失败的可预测异常
    /// </summary>
    public class FailedException : Exception
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        public string? Msg { get; set; }

        /// <summary>
        /// 是否记入日志，默认false，不记入日志
        /// </summary>
        public bool Log { get; set; } = false;
        public Exception? exception { get; set; } = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FailedException()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg">错误描述</param>
        public FailedException(string msg)
        {
            this.Msg = msg;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">错误代码</param>
        /// <param name="msg">错误描述</param>
        public FailedException(string code, string msg)
        {
            this.Code = code;
            this.Msg = msg;
        }
        public FailedException(ResponseResultCode code, string msg)
        {
            this.Code = ResultType.error.ToString();
            switch (code)
            {
                case ResponseResultCode.FAILOfConfigInit:
                    this.Msg = $"配置项初始化异常：{msg}";
                    break;
                case ResponseResultCode.FAILOfEmpty:
                    this.Msg = $"不可为空项：{msg}";
                    break;
                case ResponseResultCode.FAILOfExists:
                    this.Msg = $"值重复：{msg}";
                    break;                   
            }
            if (string.IsNullOrWhiteSpace(this.Msg))
            {
                this.Msg = msg;
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">错误代码</param>
        /// <param name="msg">错误描述</param>
        /// <param name="log">是否记入日志</param>
        public FailedException(string code, string msg, bool log)
        {
            this.Code = code;
            this.Msg = msg;
            this.Log = log;
        }
        public FailedException(string code, string msg, Exception ex, bool log = true)
        {
            this.Code = code;
            this.Msg = msg;
            this.Log = log;
            this.exception = ex;
        }


    }


    /// <summary>
    /// 业务处理失败的可预测异常（提示内容从数据库预初始化）
    /// </summary>
    public class FailedCodeException : FailedException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">错误代码</param>
        public FailedCodeException(string code)
        {
            this.Code = code;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">错误代码</param>
        /// <param name="log">此类异常 是否记入日志（一般情况不记）</param>
        public FailedCodeException(string code, bool log)
        {
            this.Code = code;
            this.Log = log;
        }
    }
}
