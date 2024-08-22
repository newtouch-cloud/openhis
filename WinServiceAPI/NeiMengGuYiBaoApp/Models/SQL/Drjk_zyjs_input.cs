using NeiMengGuYiBaoApp.Models.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.SQL
{
   public class Drjk_zyjs_input:SqlBase
    {
        /// <summary>
        /// 门诊号
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// 状态 1 正常 0作废
        /// </summary>
        public int zt { get; set; }
        /// <summary>
        /// 状态操作员
        /// </summary>
        public string zt_czy { get; set; }
        /// <summary>
        /// 状态日期
        /// </summary>
        public DateTime zt_rq { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string czydm { get; set; }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime czrq { get; set; }

        /// <summary>
        /// 结算id
        /// </summary>
        public string setl_id { get; set; }

        /// <summary>
        /// 1|人员编号|字符型|30||Y|
        /// </summary> 
        public string psn_no { get; set; }

        /// <summary>
        /// 2|就诊凭证类型|字符型|3|Y|Y|
        /// </summary> 
		public string mdtrt_cert_type { get; set; }

        /// <summary>
        /// 3|就诊凭证编号|字符型|50||Y|就诊凭证类型为“01 时填写电子凭证令牌为“02”时填写身份证号，为“03”时填写社会保障卡卡号
        /// </summary> 
		public string mdtrt_cert_no { get; set; }

        /// <summary>
        /// 4|医疗费总额|数值型|16,2||Y|
        /// </summary> 
		public string medfee_sumamt { get; set; }

        /// <summary>
        /// 5|个人结算方式|字符型|6|Y|Y|
        /// </summary> 
		public string psn_setlway { get; set; }

        /// <summary>
        /// 6|就诊ID|字符型|30||Y|
        /// </summary> 
		public string mdtrt_id { get; set; }

        /// <summary>
        /// 7|险种类型|字符型|6|Y|Y|
        /// </summary> 
		public string insutype { get; set; }

        /// <summary>
        /// 8|个人账户使用标志|字符型|1|Y|Y|
        /// </summary> 
		public string acct_used_flag { get; set; }

        /// <summary>
        /// 9|发票号|字符型|20|||
        /// </summary> 
		public string invono { get; set; }

        /// <summary>
        /// 10|中途结算标志|字符型|3|Y|Y|
        /// </summary> 
		public string mid_setl_flag { get; set; }

        /// <summary>
        /// 11|全自费金额|数值型|16,2|||
        /// </summary> 
		public decimal fulamt_ownpay_amt { get; set; }

        /// <summary>
        /// 12|超限价金额|数值型|16,2|||
        /// </summary> 
		public decimal overlmt_selfpay { get; set; }

        /// <summary>
        /// 13|先行自付金额|数值型|16,2|||
        /// </summary> 
		public decimal preselfpay_amt { get; set; }

        /// <summary>
        /// 14|符合政策范围金额|数值型|16,2|||
        /// </summary> 
		public decimal inscp_scp_amt { get; set; }


    }
}
