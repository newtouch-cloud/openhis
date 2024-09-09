using System;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_5401 : OutputBase
    {
        public Bilgiteminfo bilgiteminfo { get; set; }
    }

    public class Bilgiteminfo
    {
        /// <summary>
        /// 人员编号 (必填, 长度: 30)
        /// </summary>
        public string psn_no { get; set; }

        /// <summary>
        /// 报告单号 (必填, 长度: 30)
        /// </summary>
        public string rpotc_no { get; set; }

        /// <summary>
        /// 报告日期 (日期型)
        /// </summary>
        public DateTime? rpt_date { get; set; }

        /// <summary>
        /// 报告单类别代码 (长度: 30)
        /// </summary>
        public string rpotc_type_code { get; set; }

        /// <summary>
        /// 机构编号 (长度: 20)
        /// </summary>
        public string fixmedins_code { get; set; }

        /// <summary>
        /// 检查报告单名称 (长度: 50)
        /// </summary>
        public string exam_rpotc_name { get; set; }

        /// <summary>
        /// 检查结果阳性标志 (长度: 2)
        /// </summary>
        public string exam_rslt_poit_flag { get; set; }

        /// <summary>
        /// 检查/检验结果异常标志 (长度: 10)
        /// </summary>
        public string exam_rslt_abn { get; set; }

        /// <summary>
        /// 检查结论 (长度: 1000)
        /// </summary>
        public string examCcls { get; set; }
    }

}
