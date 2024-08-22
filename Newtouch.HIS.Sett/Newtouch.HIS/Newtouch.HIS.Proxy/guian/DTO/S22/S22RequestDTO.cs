using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO.S22
{
    /// <summary>
    /// 查询当此门诊已上传费用明细 请求报文
    /// </summary>
    [InterfaceCode("S22")]
    [XmlRoot("body")]
    public class S22RequestDTO
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
        public string outpId { get; set; }
    }
}