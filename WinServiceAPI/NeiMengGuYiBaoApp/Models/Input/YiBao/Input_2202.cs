using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_2202 : InputBase
    {
        public data2202 data { get; set; }
    }

    public class data2202
    {
        /// <summary>
        /// 人员编号 字符型 30 Y
        /// </summary>
        public string psn_no { get; set; }

        /// <summary>
        /// 就诊 ID 字符型 30 Y
        /// </summary>
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 住院/门诊号 字符型 30 Y
        /// </summary>
        public string ipt_otp_no { get; set; }

        public string expContent { get; set; }
    }
}
