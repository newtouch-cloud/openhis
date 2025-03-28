using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    public class SkintestInfoVO
    {
        /// <summary>
        /// 项目编码
        /// </summary>
        public string xmCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string xmmc { get; set; }
        /// <summary>
        /// 皮试结果
        /// </summary>
        public string result { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string remark { get; set; }
    }
}
