using System.Xml.Serialization;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_08
{
    /// <summary>
    /// 提取处方请求体
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