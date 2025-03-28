using System.Collections.Generic;
using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO.S23
{
    /// <summary>
    /// 根据S18门诊上传返回的补偿序号outpId门诊费用明细查询S22进行门诊费用明细退单；跨省如果只传补偿序号，不传明细ID 就是把所有明细删除 请求报文
    /// </summary>
    [InterfaceCode("S23")]
    [XmlRoot("body")]
    public class S23RequestDTO
    {
        /// <summary>
        /// 补偿序号
        /// </summary>
        public string outpId { get; set; }

        /// <summary>
        /// 行政编码(跨省)
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        /// 是否跨省就医患者1:是0:否(跨省)
        /// </summary>
        public string isTransProvincial { get; set; }

        /// <summary>
        /// 明细ID
        /// </summary>
        public List<string> list { get; set; }

    }
}