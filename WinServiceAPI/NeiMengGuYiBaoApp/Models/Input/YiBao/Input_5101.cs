namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_5101 : InputBase
    {
        public Data5101 data { get; set; }
    }
    public class Data5101
    {
        /// <summary>
        /// 1|交易编号|字符型|4|Y|
        /// </summary>
        public string infno { get; set; }

        /// <summary>
        /// 2|发送方报文 ID|字符型|30|Y|
        /// </summary>
        public string msgid { get; set; }

        /// <summary>
        /// 3|就医地医保区划|字符型|6|Y|
        /// </summary>
        public string mdtrtarea_admvs { get; set; }

        /// <summary>
        /// 4|参保地医保区划|字符型|6|Y|
        /// </summary>
        public string insuplc_admdvs { get; set; }

        /// <summary>
        /// 5|接收方系统代码|字符型|10|Y|
        /// </summary>
        public string recer_sys_code { get; set; }

        /// <summary>
        /// 6|设备编号|字符型|100|
        /// </summary>
        public string dev_no { get; set; }

        /// <summary>
        /// 7|设备安全信息|字符型|2000|
        /// </summary>
        public string dev_safe_info { get; set; }

        /// <summary>
        /// 8|数字签名信息|字符型|1024|
        /// </summary>
        public string cainfo { get; set; }

        /// <summary>
        /// 9|签名类型|字符型|10|Y|
        /// </summary>
        public string signtype { get; set; }

        /// <summary>
        /// 10|接口版本号|字符型|6|Y|
        /// </summary>
        public string infver { get; set; }

        /// <summary>
        /// 11|经办人类别|字符型|3|Y|Y|
        /// </summary>
        public string opter_type { get; set; }

        /// <summary>
        /// 12|经办人|字符型|30|Y|
        /// </summary>
        public string opter { get; set; }

        /// <summary>
        /// 13|经办人姓名|字符型|50|Y|
        /// </summary>
        public string opter_name { get; set; }

        /// <summary>
        /// 14|交易时间|字符型|19|Y|
        /// </summary>
        public string inf_time { get; set; }

        /// <summary>
        /// 15|定点医药机构编号|字符型|12|Y|
        /// </summary>
        public string fixmedins_code { get; set; }

        /// <summary>
        /// 16|定点医药机构名称|字符型|200|
        /// </summary>
        public string fixmedins_name { get; set; }

        /// <summary>
        /// 17|签到流水号|字符型|30|
        /// </summary>
        public string sign_no { get; set; }

    }
}
