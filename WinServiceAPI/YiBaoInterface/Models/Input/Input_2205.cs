using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_2205 : InputBase
    {
        public data2205 data { get; set; }
    }

    public class data2205
    {
        /// <summary>
        /// 就诊 ID 字符型 30 Y
        /// </summary>
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 收费批次号 字符型 30 Y 传入“0000”删除所有未结算明细
        /// </summary>
        public string chrg_bchno { get; set; }

        /// <summary>
        /// 人员编号 字符型 30 Y
        /// </summary>
        public string psn_no { get; set; }
    }
}
