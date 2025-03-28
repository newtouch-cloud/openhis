using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO.S27
{
    /// <summary>
    /// 根据S18门诊上传返回的补偿序号outpId进行获取门诊结算单信息 请求报文
    /// </summary>
    [InterfaceCode("S27")]
    [XmlRoot("body")]
    public class S27RequestDTO
    {
        /// <summary>
        /// 门诊补偿序号
        /// </summary>
        public string outpId { get; set; }
    }
}