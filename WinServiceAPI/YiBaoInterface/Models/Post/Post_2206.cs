using CQYiBaoInterface.Models.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Post
{
    public class Post_2206 
    {
        /// <summary>
        /// hisId唯一his的唯一标识
        /// </summary>
        public string hisId { get; set; }
        /// <summary>
        /// 操作员代码
        /// </summary>
        public string operatorId { get; set; }

        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string operatorName { get; set; }

        /// <summary>
        /// 参保地
        /// </summary>
        public string insuplc_admdvs { get; set; }

        /// <summary>
        /// 人员编号 字符型 30 Y
        /// </summary>
        public string psn_no { get; set; }

        /// <summary>
        /// 就诊凭证类型 字符型 3 Y Y
        /// </summary>
        public string mdtrt_cert_type { get; set; }

        /// <summary>
        /// 就诊凭证编号 字符型 50 Y  就诊凭证类型为“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
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
        /// 险种类型 字符型 6 Y Y
        /// </summary>
        public string insutype { get; set; }

        /// <summary>
        /// 个人账户使用标志 字符型 1 Y Y
        /// </summary>
        public string acct_used_flag { get; set; }

        /// <summary>
        /// 发票号 字符型 20
        /// </summary>
        public string invono { get; set; }


        /// <summary>
        /// 病种编码
        /// </summary>
        public string dise_codg { get; set; }
        /// <summary>
        /// 病种名称
        /// </summary>
        public string dise_name { get; set; }
    }
}
