using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.NeiMengGu.Output
{
    public class SettleNotifyOutput : OutputBase
    {
        /// <summary>
        ///定点医药机构本次业务流水号 字符
        /// </summary>
        public string outBizNo { get; set; }
        /// <summary>
        /// extData 扩展参数 字符 Y JSON 对象字符串
        /// </summary>
        public string extData { get; set; }
    }
}
