using Newtouch.HIS.Proxy.Attribute;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    [InterfaceCode("S11")]
    [XmlRoot("body")]
    public class S11RequestDTO
    {
        /// <summary>
        /// 开始时间(yyyy-mm-dd)
        /// </summary>
        public string startDate { get; set; }
        /// <summary>
        /// 截止时间(yyyy-mm-dd)
        /// </summary>
        public string endDate { get; set; }
        /// <summary>
        /// 补偿序号
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
    }
}