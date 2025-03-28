using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO.S03
{
    /// <summary>
    /// 根据传入的医疗证号获取家庭的参合成员列表 请求报文
    /// </summary>
    [InterfaceCode("S03")]
    [XmlRoot("body")]
    public class S03RequestDTO 
    {
        /// <summary>
        /// 参合年度
        /// </summary>
        public string year { get; set; }

        /// <summary>
        /// 医疗证号 跨省至少一个入参不能为空;不跨省，医疗证号不为空
        /// </summary>
        public string medicalNo { get; set; }

        /// <summary>
        /// 身份证号(跨省) 跨省至少一个入参不能为空;不跨省，医疗证号不为空
        /// </summary>
        public string idcardNo { get; set; }

        /// <summary>
        /// 监护人身份证号(跨省) 跨省至少一个入参不能为空;不跨省，医疗证号不为空
        /// </summary>
        public string guardianCardNo { get; set; }

        /// <summary>
        /// 行政编码(跨省)
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        /// 是否跨省就医患者1:是0:否(跨省)
        /// </summary>
        public string isTransProvincial { get; set; }
    }
}