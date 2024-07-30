using System;

namespace Newtouch.Infrastructure.ExException
{
    /// <summary>
    /// 处理过程产生的异常
    /// </summary>
    public class ProcessException : Exception
    {
        public string Msg { get; set; }

        public int ErrorCode { get; set; }

        public ProcessException() : this(0, "")
        {

        }
        public ProcessException(string msg) : this(0, msg)
        {
        }
        public ProcessException(int code) : this(code, "")
        {
        }
        public ProcessException(int code, string msg)
        {
            ErrorCode = code;
            Msg = msg;
        }
    }
}
