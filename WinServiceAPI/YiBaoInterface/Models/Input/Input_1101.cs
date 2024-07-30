using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_1101 : InputBase
    {

        public data1101 data { get; set; }


    }

    public class data1101
    {
        /// <summary>
        /// 就诊凭证类型  字符型  3  Y Y 
        /// </summary>
        [Description("就诊凭证类型")]
        public string mdtrt_cert_type { get; set; }
        /// <summary>
        /// 就诊凭证编号  字符型  50  Y  就诊凭证 类型为“01”时填 写电子凭 证令牌，为“02”时填 写身份证 号，为“03” 时填写社 会保障卡卡号
        /// </summary>
        [Description("就诊凭证编号")]
        public string mdtrt_cert_no { get; set; }

        /// <summary>
        /// 卡识别码    类型为 “03”时必 填
        /// </summary>
        [Description("卡识别码")]
        public string card_sn { get; set; }

        /// <summary>
        ///  begntime 开始时间  日期时间 获取历史
        /// </summary>
        [Description("开始时间")]
        public string begntime { get; set; }
        /// <summary>
        /// 人员证件类型  字符型  6 Y
        /// </summary>
        [Description("人员证件类型")]
        public string psn_cert_type { get; set; }

        /// <summary>
        /// 证件号码  字符型  50 
        /// </summary>
        [Description("证件号码")]
        public string certno { get; set; }

        /// <summary>
        /// psn_name 人员姓名  字符型  50
        /// </summary>
        [Description("人员姓名")]
        public string psn_name { get; set; }
    }
  
}
