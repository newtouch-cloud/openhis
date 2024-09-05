namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    /// <summary>
    /// 3503 和3503A 都用同一个
    /// </summary>
    public class Output_3504A : OutputBase
    {
        public Output3504A purcinfoErrDetail { get; set; }
    }

    public class Output3504A
    {
        /// <summary>
        /// 医疗目录编码 字符型 50 Y
        /// </summary>
        public string med_list_codg { get; set; }

        /// <summary>
        /// 定点医药机构目录编号 字符型 30 Y
        /// </summary>
        public string fixmedins_hilist_id { get; set; }

        /// <summary>
        /// 定点医药机构目录名称 字符型 200 Y
        /// </summary>
        public string fixmedins_hilist_name { get; set; }

        /// <summary>
        /// 定点医药机构批次流水号 字符型 30 Y
        /// </summary>
        public string fixmedins_bchno { get; set; }

        /// <summary>
        /// 商品采购流水号 字符型 50
        /// </summary>
        public string medins_prod_purc_no { get; set; }

        /// <summary>
        /// 返回结果 字符型 6
        /// 成功返回值为 1，失败返回值为 0。
        /// </summary>
        public string retRslt { get; set; }

        /// <summary>
        /// 错误原因 字符型 2000
        /// </summary>
        public string msgRslt { get; set; }

        /// <summary>
        /// 备注 字符型 500
        /// </summary>
        public string memo { get; set; }
    }

}
