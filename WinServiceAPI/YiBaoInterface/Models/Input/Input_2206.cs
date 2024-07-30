using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_2206 : InputBase
    {
        public data2206 data { get; set; }
    }

    public class data2206
    {
        /// <summary>
        /// 人员编号 字符型 30 Y
        /// </summary>
        public string psn_no { get; set; }

        /// <summary>
        /// 就诊凭证类型 字符型 3 Y Y
        /// </summary>
        public string mdtrt_cert_type { get; set; }

        /// <summary>
        /// 就诊凭证编号 字符型 50 Y 就诊凭证类型为“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
        /// </summary>
        public string mdtrt_cert_no { get; set; }

        /// <summary>
        /// 医疗类别 字符型 6 Y Y
        /// </summary>
        public string med_type { get; set; }

        /// <summary>
        /// 医疗费总额 数值型 16,2 Y
        /// </summary>
        public decimal medfee_sumamt { get; set; }

        /// <summary>
        /// 个人结算方式 字符型 6 Y Y
        /// </summary>
        public string psn_setlway { get; set; }

        /// <summary>
        /// 就诊 ID 字符型 30 Y
        /// </summary>
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 收费批次号 字符型 30 Y
        /// </summary>
        public string chrg_bchno { get; set; }

        /// <summary>
        /// 个人账户使用标志 字符型 1 Y Y
        /// </summary>
        public string acct_used_flag { get; set; }

        /// <summary>
        /// 险种类型 字符型 6 Y Y
        /// </summary>
        public string insutype { get; set; }
    }
}
