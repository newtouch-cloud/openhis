using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.NeiMengGu.Output
{
    public class EcAuthOutput: OutputBase
    {
        /// <summary>
        /// 实人认证业务流水号 字符 64 N 用于后续与中台交互换取身份信息
        /// </summary>
        public string authNo { get; set; }
        /// <summary>
        /// 定点医药机构本次业务流水号
        /// </summary>
        public string outBizNo { get; set; }
        /// <summary>
        /// 扩展参数 字符 N JSON 对象字符
        /// </summary>
        public string extData { get; set; }
    }
}
