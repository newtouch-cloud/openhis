using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Output
{
    public class Output_2201 : OutputBase
    {
        public data2201_out data { get; set; }
    }

    public class data2201_out
    {
        /// <summary>
        /// 就诊 ID 字符型 30 Y 医保返回唯一 流水
        /// </summary>
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 人员编号 字符型 30 Y
        /// </summary>
        public string psn_no { get; set; }

        /// <summary>
        /// 住院/门诊号 字符型 30 Y 院内唯一流水
        /// </summary>
        public string ipt_otp_no { get; set; }
    }
}
