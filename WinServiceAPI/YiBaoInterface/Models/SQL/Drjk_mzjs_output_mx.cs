using CQYiBaoInterface.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.SQL
{
    public class Drjk_mzjs_output_mx : SqlBase
    {
        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// 结算号
        /// </summary>
        public string setl_id { get; set; }


        /// <summary>
        /// 基金支付类型 字符型 6 Y Y
        /// </summary>
        public string fund_pay_type { get; set; }

        /// <summary>
        /// 符合政策范围金额 数值型 16,2 Y
        /// </summary>
        public decimal inscp_scp_amt { get; set; }

        /// <summary>
        ///本次可支付限额金额 数值型 16,2 Y
        /// </summary>
        public decimal crt_payb_lmt_amt { get; set; }

        /// <summary>
        /// 基金支付金额 数值型 16,2 Y
        /// </summary>
        public decimal fund_payamt { get; set; }

        /// <summary>
        /// 基金支付类型名称 字符型 200
        /// </summary>
        public string fund_pay_type_name { get; set; }

        /// <summary>
        /// 结算过程信息 字符型 4000
        /// </summary>
        public string setl_proc_info { get; set; }
    }
}
