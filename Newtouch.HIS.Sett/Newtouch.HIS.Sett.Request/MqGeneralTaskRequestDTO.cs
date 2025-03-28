using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request
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
        /// 操作类型  xnhS27：调用新农合S27接口、
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// 内容，action传参
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// 查询结算结果
    /// </summary>
    public class QuerySettDetail
    {
        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 补偿序号
        /// </summary>
        public string outpId { get; set; }
    }
}