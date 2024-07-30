using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO.S30
{
    /// <summary>
    /// 根据时间戳下载疾病字典信息 请求报文
    /// </summary>
    [InterfaceCode("S30")]
    [XmlRoot("body")]
    public class S30RequestDTO
    {
        /// <summary>
        /// 开始时间(yyyy-MM-dd HH:mm:ss SSS)
        /// </summary>
        public string startDate { get; set; }
    }
}