using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects
{
    /// <summary>
    /// 病历文档转化为模板
    /// </summary>
    public class BlConvertToTemplateVO
    {
        public int? mbqx { get; set; }
        public string blId { get; set; }
        //public string bllxId { get; set; }
        public string blxtmc { get; set; }
        public string bllx { get; set; }

        /// <summary>
        /// 病历文件路径
        /// </summary>
        public string blxtml { get; set; }
        public int Isempty { get; set; }
    }
}
