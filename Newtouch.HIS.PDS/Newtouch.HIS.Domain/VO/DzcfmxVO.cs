using System;

namespace Newtouch.HIS.Domain.VO
{
    /// <summary>
    /// 处方明细
    /// </summary>
    public class DzcfmxVO
    {

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// 数量（部门单位）
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 数量+单位
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// 部门单位
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 单价（部门单位）
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 剂量
        /// </summary>
        public decimal jl { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// 用法
        /// </summary>
        public string yfmc { get; set; }

        /// <summary>
        /// 医生嘱托
        /// </summary>
        public string yszt { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string ysmc { get; set; }
        public string shr { get; set; }
        public string shsj { get; set; }
        public string pc { get; set; }
    }
    public class Input_2203A
    {
        public string mdtrt_id { get; set; }
        public string psn_no { get; set; }
        public string med_type { get; set; }
        public string birctrl_type { get; set; }
        public string birctrl_matn_date { get; set; }
        public string matn_type { get; set; }
        public string dise_codg { get; set; }
        public string dise_name { get; set; }
        public string insuplc_admdvs { get; set; }
    }
}