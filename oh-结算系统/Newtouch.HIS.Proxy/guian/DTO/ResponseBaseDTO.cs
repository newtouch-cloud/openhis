using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    [XmlRoot("result")]
    public class ResponseBaseDTO<T>
    {
        public bool state { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
    [XmlRoot("result")]
    public class ResponseBaseDetailDTO<T>
    {
        public bool state { get; set; }
        public string message { get; set; }
        [XmlArrayItem("detail")]
        public T data { get; set; }
    }
    [XmlRoot("result")]
    public class ResponseBaseInpDTO<T>
    {
        public bool state { get; set; }
        public string message { get; set; }
        [XmlArrayItem("inp")]
        public T data { get; set; }
    }
}