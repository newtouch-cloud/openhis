using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_3510 : InputBase
    {
        public Data3510 data { get; set; }
    }

    public class Data3510
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
        /// 6|定点医药机构商品采购流水号|字符型|30|
        /// </summary>
        public string medins_prod_purc_no { get; set; }

        /// <summary>
        /// 7|医疗目录编码|字符型|50|
        /// </summary>
        public string med_list_codg { get; set; }

        /// <summary>
        /// 8|医药机构目录名称|字符型|100|
        /// </summary>
        public string medins_list_name { get; set; }

        /// <summary>
        /// 9|随货单号|字符型|50|
        /// </summary>
        public string dynt_no { get; set; }

        /// <summary>
        /// 10|供货商名称|字符型|200|
        /// </summary>
        public string spler_name { get; set; }

        /// <summary>
        /// 11|供应商许可证号|字符型|50|
        /// </summary>
        public string spler_pmtno { get; set; }

        /// <summary>
        /// 12|生产批号|字符型|30|
        /// </summary>
        public string manu_lotnum { get; set; }

        /// <summary>
        /// 13|生产企业名称|字符型|200|
        /// </summary>
        public string prodentp_name { get; set; }

        /// <summary>
        /// 14|批准文号|字符型|100|
        /// </summary>
        public string aprvno { get; set; }

        /// <summary>
        /// 15|生产日期|日期型|yyyy-MM-dd|
        /// </summary>
        public string manu_date { get; set; }

        /// <summary>
        /// 16|有效期止|日期型|yyyy-MM-dd|
        /// </summary>
        public string expy_end { get; set; }

        /// <summary>
        /// 17|采购发票编码|字符型|50|
        /// </summary>
        public string purc_invo_codg { get; set; }

        /// <summary>
        /// 18|采购发票号|字符型|50|
        /// </summary>
        public string purc_invo_no { get; set; }

        /// <summary>
        /// 19|处方药标志|字符型|3|Y|
        /// </summary>
        public string rx_flag { get; set; }

        /// <summary>
        /// 20|目录特项标志|字符型|3|Y|
        /// </summary>
        public string list_sp_item_flag { get; set; }

        /// <summary>
        /// 21|采购/退货入库时间|日期时间型|yyyy-MM-dd HH:mm:ss|
        /// </summary>
        public string purc_retn_stoin_time { get; set; }

        /// <summary>
        /// 22|采购/退货经办人姓名|字符型|50|
        /// </summary>
        public string purc_retn_opter_name { get; set; }

        /// <summary>
        /// 23|商品赠送标志|字符型|3|
        /// </summary>
        public string prod_geay_flag { get; set; }

        /// <summary>
        /// 24|商品退货标志|字符型|3|
        /// </summary>
        public string prod_retn_flag { get; set; }

        /// <summary>
        /// 25|备注|字符型|500|
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// 26|有效标志|字符型|3|Y|
        /// </summary>
        public string vali_flag { get; set; }

        /// <summary>
        /// 27|数据唯一记录号|字符型|40|
        /// </summary>
        public string rid { get; set; }

        /// <summary>
        /// 28|创建人 ID|字符型|20|
        /// </summary>
        public string crter_id { get; set; }

        /// <summary>
        /// 29|创建人姓名|字符型|50|
        /// </summary>
        public string crter_name { get; set; }

        /// <summary>
        /// 30|创建机构编号|字符型|20|
        /// </summary>
        public string crte_optins_no { get; set; }

        /// <summary>
        /// 31|经办人 ID|字符型|20|
        /// </summary>
        public string opter_id { get; set; }

        /// <summary>
        /// 32|经办人姓名|字符型|50|
        /// </summary>
        public string opter_name { get; set; }

        /// <summary>
        /// 33|经办机构编号|字符型|20|
        /// </summary>
        public string optins_no { get; set; }

        /// <summary>
        /// 34|统筹区编号|字符型|6|
        /// </summary>
        public string poolarea_no { get; set; }
    }


}
