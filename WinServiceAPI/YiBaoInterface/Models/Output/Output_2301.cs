using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Output
{
    public class Output_2301 : OutputBase
    {
        public List<result2301> result { get; set; }
    }

    public class result2301
    {
        /// <summary>
        /// 1|费用明细流水号|字符型|30|  ||  
        /// </summary> 
        public string feedetl_sn { get; set; }

        /// <summary>
        /// 2|明细项目费用总额|数值型|16,2|  |Y|
        /// </summary> 
		public string det_item_fee_sumamt { get; set; }

        /// <summary>
        /// 3|数量|数值型|16,4|  |Y|  
        /// </summary> 
        public string cnt { get; set; }

        /// <summary>
        /// 4|单价|数值型|16,6|  |Y|  
        /// </summary> 
        public string pric { get; set; }

        /// <summary>
        /// 5|定价上限金额|数值型|16,6|  |Y|  
        /// </summary> 
        public string pric_uplmt_amt { get; set; }

        /// <summary>
        /// 6|自付比例|数值型|5,4|  |  |  
        /// </summary> 
        public string selfpay_prop { get; set; }

        /// <summary>
        /// 7|全自费金额|数值型|16,2|  |  |  
        /// </summary> 
        public string fulamt_ownpay_amt { get; set; }

        /// <summary>
        /// 8|超限价金额|数值型|16,2|  |  |  
        /// </summary> 
        public string overlmt_amt { get; set; }

        /// <summary>
        /// 9|先行自付金额|数值型|16,2|  |  |  
        /// </summary> 
        public string preselfpay_amt { get; set; }

        /// <summary>
        /// 10|符合政策范围金额|数值型|16,2|  |  |  
        /// </summary> 
        public string inscp_scp_amt { get; set; }

        /// <summary>
        /// 11|收费项目等级|字符型|3|Y|Y|  
        /// </summary> 
        public string chrgitm_lv { get; set; }

        /// <summary>
        /// 12|医疗收费项目类别|字符型|6|Y|Y|  
        /// </summary> 
        public string med_chrgitm_type { get; set; }

        /// <summary>
        /// 13|基本药物标志|字符型|3|Y|  |  
        /// </summary> 
		public string bas_medn_flag { get; set; }

        /// <summary>
        /// 14|医保谈判药品标志|字符型|3|Y|  |  
        /// </summary> 
        public string hi_nego_drug_flag { get; set; }

        /// <summary>
        /// 15|儿童用药标志|字符型|3|Y|  |  
        /// </summary> 
		public string chld_medc_flag { get; set; }

        /// <summary>
        /// 16|目录特项标志|字符型|3|Y|  |特检特治项目或特殊药品
        /// </summary> 
        public string list_sp_item_flag { get; set; }

        /// <summary>
        /// 17|限制使用标志|字符型|3|Y|Y|
        /// </summary> 
		public string lmt_used_flag { get; set; }

        /// <summary>
        /// 18|直报标志|字符型|3|Y|  |
        /// </summary> 
        public string drt_reim_flag { get; set; }

        /// <summary>
        /// 19|备注|字符型|500|  |  |明细分割错误信息
        /// </summary> 
        public string memo { get; set; }
    }
}
