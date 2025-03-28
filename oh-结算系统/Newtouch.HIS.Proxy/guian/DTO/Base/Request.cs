using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{

    /// <summary>
    /// request dto
    /// </summary>
    /// <typeparam name="TBody"></typeparam>
    [DataContract]
    public class Request<TBody> 
    {
        public Request()
        {
            head = new RequestHeader();
        }

        /// <summary>
        /// Head
        /// </summary>
        [DataMember]
        public RequestHeader head { get; set; }

        /// <summary>
        /// body
        /// </summary>
        [DataMember]
        [XmlElement]
        public TBody body { get; set; }
    }

    /// <summary>
    /// request dto
    /// </summary>
    [DataContract]
    public class Request
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public Request()
        {
            head = new RequestHeader();
        }

        /// <summary>
        /// Head
        /// </summary>
        [DataMember]
        public RequestHeader head { get; set; }

        /// <summary>
        /// Body
        /// </summary>
        [IgnoreDataMember]
        [XmlIgnore]
        public object BodyObject { get; set; }
    }

    /// <summary>
    /// head
    /// </summary>
    [DataContract]
    [XmlRoot("head")]
    public class RequestHeader : RequestHeaderBase
    {
        /// <summary>
        /// 票据代码
        /// </summary>
        [DataMember]
        public string billCode { get; set; }
    }

    [DataContract]
    [XmlRoot("head")]
    public class RequestHeaderBase
    {
        /// <summary>
        /// 交易代码
        /// </summary>
        [DataMember]
        public string operCode { get; set; }

        /// <summary>
        /// body的md5摘要算法串
        /// </summary>
        [DataMember]
        public string rsa { get; set; }
    }
}