namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_3501 : InputBase
    {
        //public List<invinfo3501> invinfo { get; set; }
        public Invinfo3501 invinfo { get; set; }
    }

    public class Invinfo3501
    {
        /// <summary>
        /// 1|医疗目录编码|字符型|50|Y|
        /// </summary>
        public string med_list_codg { get; set; }

        /// <summary>
        /// 2|定点医药机构目录编号|字符型|30|Y|
        /// </summary>
        public string fixmedins_hilist_id { get; set; }

        /// <summary>
        /// 3|定点医药机构目录名称|字符型|200|Y|
        /// </summary>
        public string fixmedins_hilist_name { get; set; }

        /// <summary>
        /// 4|处方药标志|字符型|3|Y|
        /// </summary>
        public string rx_flag { get; set; }

        /// <summary>
        /// 5|盘存日期|日期型|Y|yyyy-MM-dd|
        /// </summary>
        public string invdate { get; set; }

        /// <summary>
        /// 6|库存数量|数值型|16,2|Y|按最小计价包装单位统计|
        /// </summary>
        public string inv_cnt { get; set; }

        /// <summary>
        /// 7|生产批号|字符型|30|
        /// </summary>
        public string manu_lotnum { get; set; }

        /// <summary>
        /// 8|定点医药机构批次流水号|字符型|30|Y|医药机构自定义批次号|
        /// </summary>
        public string fixmedins_bchno { get; set; }

        /// <summary>
        /// 9|生产日期|日期型|Y|yyyy-MM-dd|
        /// </summary>
        public string manu_date { get; set; }

        /// <summary>
        /// 10|有效期止|日期型|Y|yyyy-MM-dd|
        /// </summary>
        public string expy_end { get; set; }

        /// <summary>
        /// 11|备注|字符型|500|
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// 12|溯源码节点信息|嵌套对象|
        /// </summary>
        public Drugtracinfo drugtracinfo { get; set; }
    }

    public class Drugtracinfo
    {
        /// <summary>
        /// 药品追溯码|字符型|100
        /// </summary>
        public string drug_trac_codg { get; set; }
    }
}
