using Newtouch.HIS.Proxy.Attribute;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    [InterfaceCode("S17")]
    [XmlRoot("body")]
    public class S17RequestDTO
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public string startDate { get; set; }
        /// <summary>
        /// 截止时间
        /// </summary>
        public string endDate { get; set; }
        /// <summary>
        /// 医疗证卡号
        /// </summary>
        public string medicalNo { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string memberName { get; set; }
    }
}