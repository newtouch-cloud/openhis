namespace NeiMengGuYiBaoApp.Models.Post.YiBao
{
    public class Post_3513 : PostBase
    {

        /// <summary>
        /// 1|定点医药机构编号|字符型|30|Y
        /// </summary>
        public string fixmedins_code { get; set; }

        /// <summary>
        /// 2|医药机构目录编码|字符型|150|N
        /// </summary>
        public string medins_list_codg { get; set; }

        /// <summary>
        /// 3|定点医药机构批次流水号|字符型|30|N
        /// </summary>
        public string fixmedins_bchno { get; set; }

        /// <summary>
        /// 4|开始日期|日期型|yyyy-MM-dd|N
        /// </summary>
        public string begndate { get; set; }

        /// <summary>
        /// 5|结束日期|日期型|yyyy-MM-dd|N
        /// </summary>
        public string enddate { get; set; }

        /// <summary>
        /// 7|医疗目录编码|字符型|50|N
        /// </summary>
        public string med_list_codg { get; set; }

        /// <summary>
        /// 8|就诊 ID|字符型|30|N
        /// </summary>
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 9|人员编号|字符型|30|N
        /// </summary>
        public string psn_no { get; set; }

        /// <summary>
        /// 10|人员证件类型|字符型|6|Y
        /// </summary>
        public string psn_cert_type { get; set; }

        /// <summary>
        /// 11|证件号码|字符型|600|N
        /// </summary>
        public string certno { get; set; }

        /// <summary>
        /// 12|人员姓名|字符型|50|N
        /// </summary>
        public string psn_name { get; set; }

        /// <summary>
        /// 13|药品追溯码|字符型|30|N
        /// </summary>
        public string drug_trac_codg { get; set; }
    }



}
