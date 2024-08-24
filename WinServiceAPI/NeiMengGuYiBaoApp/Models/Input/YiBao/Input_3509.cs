using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_3509 : InputBase
    {
        public Data3509 data { get; set; }
    }

    public class Data3509
    {
        /// <summary>
        /// 1|定点医药机构编号|字符型|30|Y|
        /// </summary>
        public string fixmedins_code { get; set; }

        /// <summary>
        /// 2|医药机构目录编码|字符型|150|Y|
        /// 医药机构目录编码、定点医药机构批次流水号中,必须传其中一个字段
        /// </summary>
        public string medins_list_codg { get; set; }

        /// <summary>
        /// 3|定点医药机构批次流水号|字符型|30|Y|
        /// 医药机构目录编码、定点医药机构批次流水号中,必须传其中一个字段
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
        /// 6|定点医药机构商品库存变更流水号|字符型|30|
        /// </summary>
        public string medins_prod_inv_chg_no { get; set; }

        /// <summary>
        /// 7|医疗目录编码|字符型|50|
        /// </summary>
        public string med_list_codg { get; set; }

        /// <summary>
        /// 8|库存变更类型|字符型|6|Y|
        /// </summary>
        public string inv_chg_type { get; set; }

        /// <summary>
        /// 9|医药机构目录名称|字符型|100|
        /// </summary>
        public string medins_list_name { get; set; }

        /// <summary>
        /// 10|处方药标志|字符型|3|Y|
        /// </summary>
        public string rx_flag { get; set; }

        /// <summary>
        /// 11|目录特项标志|字符型|3|Y|
        /// </summary>
        public string list_sp_item_flag { get; set; }

        /// <summary>
        /// 12|拆零标志|字符型|3|Y|
        /// </summary>
        public string trdn_flag { get; set; }

        /// <summary>
        /// 13|库存变更时间|日期时间型|yyyy-MM-dd HH:mm:ss|
        /// </summary>
        public string inv_chg_time { get; set; }

        /// <summary>
        /// 14|库存变更经办人姓名|字符型|50|
        /// </summary>
        public string inv_chg_opter_name { get; set; }

        /// <summary>
        /// 15|备注|字符型|500|
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// 16|有效标志|字符型|3|Y|
        /// </summary>
        public string vali_flag { get; set; }

        /// <summary>
        /// 17|数据唯一记录号|字符型|40|
        /// </summary>
        public string rid { get; set; }

        /// <summary>
        /// 18|创建人 ID|字符型|20|
        /// </summary>
        public string crter_id { get; set; }

        /// <summary>
        /// 19|创建人姓名|字符型|50|
        /// </summary>
        public string crter_name { get; set; }

        /// <summary>
        /// 20|创建机构编号|字符型|20|
        /// </summary>
        public string crte_optins_no { get; set; }

        /// <summary>
        /// 21|经办人 ID|字符型|20|
        /// </summary>
        public string opter_id { get; set; }

        /// <summary>
        /// 22|经办人姓名|字符型|50|
        /// </summary>
        public string opter_name { get; set; }

        /// <summary>
        /// 23|经办机构编号|字符型|20|
        /// </summary>
        public string optins_no { get; set; }

        /// <summary>
        /// 24|统筹区编号|字符型|6|
        /// </summary>
        public string poolarea_no { get; set; }
    }


}
