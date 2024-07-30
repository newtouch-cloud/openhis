using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class InputPost2<I>
    {
        [Description("应用ID")]
        public string appId { get; set; }
        [Description("加密算法")]
        public string encType { get; set; }
        [Description("签名算法")]
        public string signType { get; set; }
        [Description("时间串")]
        public string timestamp { get; set; }
        [Description("交易输入")]
        public string encData { get; set; }
        [Description("签名报文")]
        public string signData { get; set; }
    }
}
