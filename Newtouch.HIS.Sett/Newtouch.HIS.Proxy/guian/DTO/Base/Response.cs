using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    /// <summary>
    /// response
    /// </summary>
    [XmlRoot("result")]
    public class Response<T>
    {
        /// <summary>
        /// 交易结果状态true成功false失败， 必填  
        /// </summary>
        public bool state { get; set; }

        /// <summary>
        /// 失败原因
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public T data { get; set; }
    }
}