namespace NewtouchHIS.Lib.Base.Model
{
    /// <summary>
    /// API响应
    /// </summary>
    public class ResponseBase
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
        /// 响应数据（ResponseResultCode.SUCCESS时的返回数据）
        /// </summary>
        public object? data { get; set; }

    }
    /// <summary>
    /// 过渡类 仅用于调用his内部Api
    /// </summary>
    public class ResponseBaseOld : ResponseBase
    {

        public string sub_code { get; set; }
        public string sub_msg { get; set; }
    }


    public enum ResponseResultCode
    {
        /// <summary>
        /// 默认值
        /// </summary>
        Default = -1,
        /// <summary>
        /// 成功
        /// </summary>
        SUCCESS = 10000,

        /// <summary>
        /// 服务器内部错误（程序发生异常）
        /// </summary>
        ERROR = 20000,
        /// <summary>
        /// Token无效
        /// </summary>
        INVALIDTOKEN = 40000,
        /// <summary>
        /// 鉴权失败
        /// </summary>
        FAILOfAuth = 40001,
        /// <summary>
        /// 重复请求
        /// </summary>
        FAILOfRequestRepeat = 40002,
        /// <summary>
        /// 业务处理失败:	具体失败原因参见接口返回的错误码    
        /// </summary>
        FAIL = 40004,
        /// <summary>
        /// 配置初始化异常
        /// </summary>
        FAILOfConfigInit=400040,
        /// <summary>
        /// 密码错误
        /// </summary>
        FAILOfPwdError = 400041,
        /// <summary>
        /// 空值校验
        /// </summary>
        FAILOfEmpty = 410001,
        /// <summary>
        /// 重复值校验
        /// </summary>
        FAILOfExists = 410002,

    }
}
