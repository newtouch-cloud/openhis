using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO.S19
{
    /// <summary>
    /// 根据S18上传门诊返回的补偿序号outpId进行门诊回退 请求报文
    /// </summary>
    [InterfaceCode("S19")]
    [XmlRoot("body")]
    public class S19RequestDTO
    {
        /// <summary>
        /// 补偿序号
        /// </summary>
        public string outpId { get; set; }

        /// <summary>
        ///  门诊登记取消原因(跨省)
        /// </summary>
        public string cancelCause { get; set; }

        /// <summary>
        ///  行政编码(跨省)
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        /// 是否跨省就医患者1:是0:否(跨省)
        /// </summary>
        public string isTransProvincial { get; set; }

    }
}