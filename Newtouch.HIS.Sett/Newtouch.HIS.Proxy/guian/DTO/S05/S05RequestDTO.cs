using Newtouch.HIS.Proxy.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    [InterfaceCode("S05")]
    [XmlRoot("body")]
    public class S05RequestDTO
    {
        /// <summary>
        /// 补偿序号
        /// </summary>
        public string inpId { get; set; }
        /// <summary>
        /// 住院登记取消原因(跨省)
        /// </summary>
        public string cancelCause { get; set; }
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
