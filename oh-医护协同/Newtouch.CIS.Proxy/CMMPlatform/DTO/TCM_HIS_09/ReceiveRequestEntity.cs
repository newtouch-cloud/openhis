using System.Xml.Serialization;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_09
{
    /// <summary>
    /// 提取诊断信息请求体
    /// </summary>
    [XmlRoot("Request")]
    public class ReceiveRequestEntity : RequestBase
    {
        /// <summary>
        /// 接收信息请求体
        /// </summary>
        public Receive Receive { get; set; }

    }
}