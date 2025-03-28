using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    /// <summary>
    /// request head dto
    /// </summary>
    [XmlRoot("head")]
    public class RequestHeadDTO: RequestBaseHeadDTO
    {
        public string billCode { get; set; }
    }

    /// <summary>
    /// request head dto
    /// </summary>
    [XmlRoot("head")]
    public class RequestBaseHeadDTO
    {
        public string operCode { get; set; }
        public string rsa { get; set; }
    }
}
