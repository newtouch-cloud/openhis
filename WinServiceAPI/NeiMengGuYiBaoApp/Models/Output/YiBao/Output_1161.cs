using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_1161 : Output_1101
    {

        /// <summary>
        /// 人员证件类型 字符型 6 Y Y
        /// </summary>
        //public string psn_cert_type { get; set; }

        /// <summary>
        /// 证件号码 字符型 50 Y
        /// </summary>
        //public string certno { get; set; }

        /// <summary>
        /// 人员姓名 字符型 50 Y
        /// </summary>
        //public string psn_name { get; set; }

        /// <summary>
        ///  社保卡卡号 字符型 20 读卡时返回
        /// </summary>
        public string cardno { get; set; }

        /// <summary>
        ///  卡识别码 字符型 32 读卡时返回
        /// </summary>
        public string card_sn { get; set; }

        /// <summary>
        /// 令牌 字符型 50 读电子凭证时返回
        /// </summary>
        public string ecToken { get; set; }
    }
}
