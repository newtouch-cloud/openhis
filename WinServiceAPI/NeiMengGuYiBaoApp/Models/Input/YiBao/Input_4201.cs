using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_4201 : InputBase
    {
        public feedetail_4201 feedetail { get; set; }
    }

    public class feedetail_4201
    {

        /// <summary>
        /// 1|就医流水号|字符型|30|||Y
        /// </summary>
        public string mdtrt_sn { get; set; }

        /// <summary>
        /// 2|住院/门诊号|字符型|30|||Y
        /// </summary>
        public string ipt_otp_no { get; set; }

        /// <summary>
        /// 3|医疗类别|字符型|6|Y|Y
        /// </summary>
        public string med_type { get; set; }

        /// <summary>
        /// 4|收费批次号|字符型|30|||Y
        /// </summary>
        public string chrg_bchno { get; set; }

        /// <summary>
        /// 5|费用明细流水号|字符型|30|||Y
        /// </summary>
        public string feedetl_sn { get; set; }

        /// <summary>
        /// 6|人员证件类型|字符型|6|Y|Y
        /// </summary>
        public string psn_cert_type { get; set; }

        /// <summary>
        /// 7|证件号码|字符型|50|||Y
        /// </summary>
        public string certno { get; set; }

        /// <summary>
        /// 8|人员姓名|字符型|50|||Y
        /// </summary>
        public string psn_name { get; set; }

        /// <summary>
        /// 9|费用发生时间|日期时间型|||Y
        /// </summary>
        public string fee_ocur_time { get; set; }

        /// <summary>
        /// 10|数量|数值型|16,4|||Y
        /// </summary>
        public decimal cnt { get; set; }

        /// <summary>
        /// 11|单价|数值型|16,6|||Y
        /// </summary>
        public decimal pric { get; set; }

        /// <summary>
        /// 12|明细项目费用总额|数值型|16,2|||Y
        /// </summary>
        public decimal det_item_fee_sumamt { get; set; }

        /// <summary>
        /// 13|医疗目录编码|字符型|50|||Y
        /// </summary>
        public string med_list_codg { get; set; }

        /// <summary>
        /// 14|医药机构目录编码|字符型|150|||Y
        /// </summary>
        public string medins_list_codg { get; set; }

        /// <summary>
        /// 15|医药机构目录名称|字符型|100|||Y
        /// </summary>
        public string medins_list_name { get; set; }

        /// <summary>
        /// 16|医疗收费项目类别|字符型|6|Y|Y
        /// </summary>
        public string med_chrgitm_type { get; set; }

        /// <summary>
        /// 17|商品名|字符型|200|||Y
        /// </summary>
        public string prodname { get; set; }

        /// <summary>
        /// 18|开单科室编码|字符型|30|||Y
        /// </summary>
        public string bilg_dept_codg { get; set; }

        /// <summary>
        /// 19|开单科室名称|字符型|100|||Y
        /// </summary>
        public string bilg_dept_name { get; set; }

        /// <summary>
        /// 20|开单医生编码|字符型|30|||Y
        /// </summary>
        public string bilg_dr_codg { get; set; }

        /// <summary>
        /// 21|开单医师姓名|字符型|50|||Y
        /// </summary>
        public string bilg_dr_name { get; set; }

        /// <summary>
        /// 22|受单科室编码|字符型|30||| 
        /// </summary>
        public string accord_dept_codg { get; set; }

        /// <summary>
        /// 23|受单科室名称|字符型|100||| 
        /// </summary>
        public string accord_dept_name { get; set; }

        /// <summary>
        /// 24|受单医生编码|字符型|30||| 
        /// </summary>
        public string orders_dr_code { get; set; }

        /// <summary>
        /// 25|受单医生姓名|字符型|50||| 
        /// </summary>
        public string orders_dr_name { get; set; }

        /// <summary>
        /// 26|中药使用方式|字符型|6||| 
        /// </summary>
        public string tcmdrug_used_way { get; set; }

        /// <summary>
        /// 27|外检标志|字符型|3||| 
        /// </summary>
        public string etip_flag { get; set; }

        /// <summary>
        /// 28|外检医院编码|字符型|30||| 
        /// </summary>
        public string etip_hosp_code { get; set; }

        /// <summary>
        /// 29|出院带药标志|字符型|3|||Y
        /// </summary>
        public string dscg_tkdrug_flag { get; set; }

        /// <summary>
        /// 30|备注|字符型|500||| 
        /// </summary>
        public string memo { get; set; }

    }
}
