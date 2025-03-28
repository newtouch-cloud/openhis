using Newtouch.HIS.Proxy.Attribute;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    [InterfaceCode("S12")]
    [XmlRoot("body")]
    public class S12RequestDTO
    {
        /// <summary>
        /// 补偿序号（跨省删除部分明细需要传单条明细的序号，如果不传明细序号删除所有明细）
        /// </summary>
        public string inpId { get; set; }
        /// <summary>
        /// 行政编码(跨省)
        /// </summary>
        public string areaCode { get; set; }
        /// <summary>
        /// 是否跨省就医患者1:是0:否(跨省)
        /// </summary>
        public string isTransProvincial { get; set; }
        [XmlArray("list"), XmlArrayItem("string")]
        public List<string> list { get; set; }
    }
}