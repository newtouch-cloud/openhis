using System;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_4208 : InputBase
    {
        // 自费病人就诊信息
        public Input4208 input { get; set; }
    }

    /// <summary>
    /// 自费病人就诊信息（节点标识：input）
    /// </summary>
    public class Input4208
    {
        /// <summary>
        /// 人员证件类型 (长度: 6)
        /// </summary>
        public string psn_cert_type { get; set; }

        /// <summary>
        /// 证件号码 (长度: 600)
        /// </summary>
        public string certno { get; set; }

        /// <summary>
        /// 人员姓名 (长度: 50)
        /// </summary>
        public string psn_name { get; set; }

        /// <summary>
        /// 开始时间 (格式: yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public DateTime begntime { get; set; }

        /// <summary>
        /// 结束时间 (格式: yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public DateTime endtime { get; set; }

        /// <summary>
        /// 医疗类别 (长度: 6)
        /// </summary>
        public string med_type { get; set; }

        /// <summary>
        /// 医疗总费用 (数值型: 16,2)
        /// </summary>
        public decimal medfee_sumamt { get; set; }

        /// <summary>
        /// 电子票据号码 (长度: 50)
        /// </summary>
        public string elec_billno_code { get; set; }

        /// <summary>
        /// 完成标志 (长度: 6)
        /// </summary>
        public string cplt_flag { get; set; }

        /// <summary>
        /// 当前页数 (数值型: 4)
        /// </summary>
        public int page_num { get; set; }

        /// <summary>
        /// 本页数据量 (数值型: 4)
        /// </summary>
        public int page_size { get; set; }
    }

}
