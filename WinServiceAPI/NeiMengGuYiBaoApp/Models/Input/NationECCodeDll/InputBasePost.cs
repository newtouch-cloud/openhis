using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.NationECCodeDll
{
    public class InputBasePost
    {
        [Description("机构ID-机构代码")]
        public string orgId { get; set; }

        [Description("交易类型-接口交易代码")]
        public string transType { get; set; }

        [Description("接口请求参数-JSON 格式字符")]
        public JObject data { get; set; }

        [Description("扩展参数-JSON 格式字符")]
        public string extra { get; set; }
    }
}
