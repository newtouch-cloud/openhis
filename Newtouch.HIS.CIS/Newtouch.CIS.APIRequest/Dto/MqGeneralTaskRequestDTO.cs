using Newtouch.HIS.API.Common;

namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// 通用消息请求报文
    /// </summary>
    public class MqGeneralTaskRequestDto : RequestBase
    {
        /// <summary>
        /// 消息主内容
        /// </summary>
        public string body { get; set; }
    }

    /// <summary>
    /// MqGeneralTaskRequestDto.body反序列化内容
    /// </summary>
    public class MqGeneralTaskBody
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// 内容，action传参
        /// </summary>
        public string Content { get; set; }
    }
}