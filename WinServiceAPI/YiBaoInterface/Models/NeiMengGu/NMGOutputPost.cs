using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai
{
    public class NMGOutputPost
    {
        [Description("机构ID-机构代码")]
        public string orgId { get; set; }

        [Description("接口返回值非 0 时,该出参为交易錯误信息,详见表 A.3 返回值代码表")]
        public int code { get; set; }

        [Description("code 非 0 时有效")]
        public string message { get; set; }

        [Description("接口响应参数-JSON 格式字符")]
        public string data { get; set; }

        [Description("扩展参数-JSON 格式字符")]
        public string extra { get; set; }
    }
}
