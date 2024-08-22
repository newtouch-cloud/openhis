using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO.S25
{
    /// <summary>
    /// 根据S18门诊上传返回的补偿序号outpId进行门诊结算 请求报文
    /// </summary>
    [InterfaceCode("S25")]
    [XmlRoot("body")]
    public class S25RequestDTO
    {
        /// <summary>
        /// 补偿序号
        /// </summary>
        public string outpId { get; set; }

        /// <summary>
        ///补偿类型编码(跨省)
        /// </summary>
        public string redeemNo { get; set; }

        /// <summary>
        ///出院日期(yyyy-mm-dd hh:mm:ss )(跨省, 可传当前时间)
        /// </summary>
        public string leaveDate { get; set; }

        /// <summary>
        ///行政编码(跨省)
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        ///是否跨省就医患者1:是0:否(跨省)
        /// </summary>
        public string isTransProvincial { get; set; }
    }
}