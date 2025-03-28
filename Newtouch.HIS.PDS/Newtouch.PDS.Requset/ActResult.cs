using System.ComponentModel;

namespace Newtouch.PDS.Requset
{
    /// <summary>
    /// 执行结果
    /// </summary>
    public class ActResult
    {
        /// <summary>
        /// 执行结果
        /// </summary>
        public ActResult()
        {
            IsSucceed = true;
            ResultCode = 0;
            ResultMsg = string.Empty;
        }

        /// <summary>
        /// 是否成功 true:全部成功/部分成功  false:全部失败/验证失败
        /// </summary>
        public bool IsSucceed { get; set; }

        /// <summary>
        /// 状态码 参照枚举ResultCode
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        public string ResultMsg { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public dynamic Data { get; set; }
    }

    /// <summary>
    /// 返回状态枚举
    /// </summary>
    public enum ResultCode
    {
        /// <summary>
        /// 执行成功
        /// </summary>
        [Description("执行成功")]
        None = 0,

        /// <summary>
        /// 执行成功
        /// </summary>
        [Description("部分成功")]
        PartSuccess = 1,

        /// <summary>
        /// 执行成功
        /// </summary>
        [Description("部分成功")]
        AllFailure = 2,

        /// <summary>
        /// 验证失败
        /// </summary>
        [Description("验证失败")]
        ValidationFailure = 3,

        /// <summary>
        /// 内部处理异常
        /// </summary>
        [Description("内部处理异常")]
        InternalProcessingAbnormality = 4,

        /// <summary>
        /// 其他错误
        /// </summary>
        [Description("其他错误")]
        Other = 5,
    }
}
