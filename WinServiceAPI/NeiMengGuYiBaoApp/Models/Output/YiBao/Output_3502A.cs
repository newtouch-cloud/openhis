using System.Collections.Generic;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_3502A : OutputBase
    {
        public List<Output3502A> invinfoErrDetail { get; set; }
    }

    public class Output3502A
    {
        /// <summary>
        /// 医疗目录编码 字符型 50 Y
        /// </summary>
        public string med_list_codg { get; set; }

        /// <summary>
        /// 库存变更类型 字符型 6 Y Y
        /// </summary>
        public string inv_chg_type { get; set; }

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
        /// 返回结果 字符型 6 
        /// 成功返回值为1，失败返回值为0。
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
