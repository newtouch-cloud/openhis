using Newtouch.HIS.Proxy.Attribute;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    [InterfaceCode("S13")]
    [XmlRoot("body")]
    public class S13RequestDTO
    {
        /// <summary>
        /// 补偿序号
        /// </summary>
        public string inpId { get; set; }
        /// <summary>
        /// 补偿类型编码
        /// </summary>
        public string redeemNo { get; set; }
        /// <summary>
        /// 出院时间(跨省)
        /// </summary>
        public string outDate { get; set; }
        /// <summary>
        /// 五保户、低保户等特殊参合患者是否提供身份属性证明材料(跨省)1：已提供 0：未提供
        /// </summary>
        public string isMaterials { get; set; }
        /// <summary>
        /// 操作员姓名(跨省)
        /// </summary>
        public string operationName { get; set; }
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