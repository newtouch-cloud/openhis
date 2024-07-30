namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// 执行结果
    /// </summary>
    public class ActResultDTO
    {
        /// <summary>
        /// 构造函数初始化
        /// </summary>
        public ActResultDTO()
        {
            IsSucceed = true;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSucceed { get; set; }

        /// <summary>
        /// 错误编码
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        public string ErrorMsg { get; set; }
    }
}