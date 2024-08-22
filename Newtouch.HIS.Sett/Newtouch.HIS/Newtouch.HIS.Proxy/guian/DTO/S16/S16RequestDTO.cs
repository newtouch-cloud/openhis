using Newtouch.HIS.Proxy.Attribute;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO.S16
{
    [InterfaceCode("S16")]
    [XmlRoot("body")]
    public class S16RequestDTO
    {
        /// <summary>
        /// 住院补偿序号
        /// </summary>
        public string inpId { get; set; }
    }
}