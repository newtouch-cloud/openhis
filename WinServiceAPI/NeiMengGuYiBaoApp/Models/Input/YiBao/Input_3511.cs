using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_3511 : InputBase
    {
        public Data3511 data { get; set; }
    }


    public class Data3511
    {
        /// <summary>
        /// 1|定点医药机构编号|字符型|30|Y|
        /// </summary>
        public string fixmedins_code { get; set; }

        /// <summary>
        /// 2|医药机构目录编码|字符型|150|Y|医药机构目录编码、定点医药机构批次流水号中,必须传其中一个字段|
        /// </summary>
        public string medins_list_codg { get; set; }

        /// <summary>
        /// 3|定点医药机构批次流水号|字符型|30|Y|医药机构目录编码、定点医药机构批次流水号中,必须传其中一个字段|
        /// </summary>
        public string fixmedins_bchno { get; set; }

        /// <summary>
        /// 4|开始日期|日期型|yyyy-MM-dd|Y|
        /// </summary>
        public string begndate { get; set; }

        /// <summary>
        /// 5|结束日期|日期型|yyyy-MM-dd|Y|
        /// </summary>
        public string enddate { get; set; }

        /// <summary>
        /// 6|定点医药机构商品销售流水号|字符型|30|
        /// </summary>
        public string medins_prod_sel_no { get; set; }

        /// <summary>
        /// 7|医疗目录编码|字符型|50|
        /// </summary>
        public string med_list_codg { get; set; }

        /// <summary>
        /// 8|医药机构目录名称|字符型|100|
        /// </summary>
        public string medins_list_name { get; set; }

        /// <summary>
        /// 9|开单医师证件类型|字符型|3|
        /// </summary>
        public string prsc_dr_cert_type { get; set; }

        /// <summary>
        /// 10|开单医师证件号码|字符型|50|
        /// </summary>
        public string prsc_dr_certno { get; set; }

        /// <summary>
        /// 11|开单医师姓名|字符型|50|
        /// </summary>
        public string bilg_dr_name { get; set; }

        /// <summary>
        /// 12|药师证件类型|字符型|3|
        /// </summary>
        public string phar_cert_type { get; set; }

        /// <summary>
        /// 13|药师证件号码|字符型|50|
        /// </summary>
        public string phar_certno { get; set; }

        /// <summary>
        /// 14|药师姓名|字符型|50|
        /// </summary>
        public string phar_name { get; set; }

        /// <summary>
        /// 15|药师执业资格证号|字符型|50|
        /// </summary>
        public string phar_prac_cert_no { get; set; }

        /// <summary>
        /// 16|医保费用结算类型|字符型|6|Y|
        /// </summary>
        public string hi_feesetl_type { get; set; }

        /// <summary>
        /// 17|结算 ID|字符型|30|
        /// </summary>
        public string setl_id { get; set; }

        /// <summary>
        /// 18|人员编号|字符型|30|
        /// </summary>
        public string psn_no { get; set; }

        /// <summary>
        /// 19|人员证件类型|字符型|6|Y|
        /// </summary>
        public string psn_cert_type { get; set; }

        /// <summary>
        /// 20|证件号码|字符型|600|
        /// </summary>
        public string certno { get; set; }

        /// <summary>
        /// 21|人员姓名|字符型|50|
        /// </summary>
        public string psn_name { get; set; }

        /// <summary>
        /// 22|生产批号|字符型|30|
        /// </summary>
        public string manu_lotnum { get; set; }

        /// <summary>
        /// 23|生产日期|日期型|yyyy-MM-dd|
        /// </summary>
        public string manu_date { get; set; }

        /// <summary>
        /// 24|有效期止|日期型|yyyy-MM-dd|
        /// </summary>
        public string expy_end { get; set; }

        /// <summary>
        /// 25|电子监管编码|字符型|20|
        /// </summary>
        public string elec_supn_codg { get; set; }

        /// <summary>
        /// 26|处方药标志|字符型|3|Y|
        /// </summary>
        public string rx_flag { get; set; }

        /// <summary>
        /// 27|目录特项标志|字符型|3|Y|
        /// </summary>
        public string list_sp_item_flag { get; set; }

        /// <summary>
        /// 28|拆零标志|字符型|3|Y|
        /// </summary>
        public string trdn_flag { get; set; }

        /// <summary>
        /// 29|销售/退货时间|日期时间型|yyyy-MM-dd HH:mm:ss|
        /// </summary>
        public string sel_retn_time { get; set; }

        /// <summary>
        /// 30|销售/退货经办人姓名|字符型|50|
        /// </summary>
        public string sel_retn_opter_name { get; set; }

        /// <summary>
        /// 31|备注|字符型|500|
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// 32|有效标志|字符型|3|Y|
        /// </summary>
        public string vali_flag { get; set; }

        /// <summary>
        /// 33|数据唯一记录号|字符型|40|
        /// </summary>
        public string rid { get; set; }

        /// <summary>
        /// 34|创建人 ID|字符型|20|
        /// </summary>
        public string crter_id { get; set; }

        /// <summary>
        /// 35|创建人姓名|字符型|50|
        /// </summary>
        public string crter_name { get; set; }

        /// <summary>
        /// 36|创建机构编号|字符型|20|
        /// </summary>
        public string crte_optins_no { get; set; }

        /// <summary>
        /// 37|经办人 ID|字符型|20|
        /// </summary>
        public string opter_id { get; set; }

        /// <summary>
        /// 38|经办人姓名|字符型|50|
        /// </summary>
        public string opter_name { get; set; }

        /// <summary>
        /// 39|经办机构编号|字符型|20|
        /// </summary>
        public string optins_no { get; set; }

        /// <summary>
        /// 40|统筹区编号|字符型|6|
        /// </summary>
        public string poolarea_no { get; set; }
    }


}
