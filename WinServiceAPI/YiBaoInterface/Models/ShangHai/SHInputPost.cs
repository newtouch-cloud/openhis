using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai
{
    public class SHInputPost<I>
    {
        [Description("交易时间")]
        public string jysj { get; set; }

        [Description("消息类型码")]
        public string xxlxm { get; set; }

        [Description("交易返回码")]
        public string xxfhm { get; set; }

        [Description("交易返回信息")]
        public string fhxx { get; set; }

        [Description("交易版本号")]
        public string bbh { get; set; }

        [Description("报文 ID")]
        public string msgid { get; set; }

        [Description("发卡地行政区划代码")]
        public string xzqhdm { get; set; }

        [Description("医疗机构代码")]
        public string jgdm { get; set; }

        [Description("操作员编码")]
        public string czybm { get; set; }

        [Description("操作员姓名")]
        public string czyxm { get; set; }

        [Description("消息内容")]
        public I xxnr { get; set; }

        [Description("交易渠道")]
        public string jyqd { get; set; }
        [Description("交易验证码")]
        public string jyyzm { get; set; }
        [Description("终端设备识别码")]
        public string zdjbhs { get; set; }
    }
}
