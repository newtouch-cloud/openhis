using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO.S02
{
    /// <summary>
    /// 客户端传入用户名密码进行身份认证返回票据代码，密码需要MD5加密32位大写 request dto
    /// </summary>
    [XmlRoot("body")]
    public class S02RequestDTO
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string passWord { get; set; }
    }
}
