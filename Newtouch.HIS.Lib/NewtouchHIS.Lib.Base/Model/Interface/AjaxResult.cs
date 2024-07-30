namespace NewtouchHIS.Lib.Base.Model
{
    /// <summary>
    /// Ajax响应内容 Model
    /// </summary>
    public class AjaxResult
    {
        /// <summary>
        /// 操作结果类型
        /// </summary>
        public object state { get; set; }

        /// <summary>
        /// 获取 消息内容
        /// </summary>
        public string? message { get; set; }

        /// <summary>
        /// 获取 返回数据
        /// </summary>
        public object? data { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string? code { get; set; }

        /// <summary>
        /// 异常的堆栈轨迹
        /// </summary>
        public string? exStackTrace { get; set; }

        /// <summary>
        /// InnerException
        /// </summary>
        public Exception? InnerException { get; set; }
    }

    /// <summary>
    /// 表示 ajax 操作结果类型的枚举
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// 消息结果类型
        /// </summary>
        info,
        /// <summary>
        /// 成功结果类型
        /// </summary>
        success,
        /// <summary>
        /// 警告结果类型
        /// </summary>
        warning,
        /// <summary>
        /// 异常结果类型
        /// </summary>
        error
    }
}
