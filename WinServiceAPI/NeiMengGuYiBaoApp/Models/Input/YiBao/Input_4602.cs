using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    /// <summary>
    /// 【4602】护理操作生命体征测量记录
    /// </summary>
    public class Input_4602: InputBase
    {
        public List<data4602> data { get; set; }
    }
    public class data4602 {
        /// <summary>
        /// 就医流水号
        /// </summary>
        public string mdtrt_sn { get; set; }
        /// <summary>
        /// 就诊 ID
        /// </summary>
        public string mdtrt_id { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public string psn_no { get; set; }
        /// <summary>
        /// 科室代码
        /// </summary>
        public string dept_code { get; set; }
        /// <summary>
        ///科室名称
        /// </summary>
        public string dept_name { get; set; }
        /// <summary>
        /// 病区名称
        /// </summary>
        public string wardarea_name { get; set; }
        /// <summary>
        /// 病床号
        /// </summary>
        public string bedno { get; set; }
        /// <summary>
        ///诊断代码
        /// </summary>
        public string diag_code { get; set; }
        /// <summary>
        /// 入院时间
        /// </summary>
        public string adm_time { get; set; }
        /// <summary>
        /// 实际住院天数
        /// </summary>
        public int act_ipt_days { get; set; }
        /// <summary>
        /// 手术后天数
        /// </summary>
        public int afpn_days { get; set; }
        /// <summary>
        /// 记录日期时间
        /// </summary>
        public string rcd_time { get; set; }
        /// <summary>
        /// 呼吸频率（次/min）
        /// </summary>
        public int vent_frqu { get; set; }
        /// <summary>
        /// 使用呼吸机标志
        /// </summary>
        public string use_vent_flag { get; set; }
        /// <summary>
        /// 脉率（次/min）
        /// </summary>
        public int pule { get; set; }
        /// <summary>
        /// 起搏器心率（次/min）
        /// </summary>
        public int pat_heart_rate { get; set; }
        /// <summary>
        /// 体温（℃）
        /// </summary>
        public decimal tprt { get; set; }
        /// <summary>
        /// 收缩压（mmHg）
        /// </summary>
        public string systolic_pre { get; set; }
        /// <summary>
        /// 舒张压（mmHg）
        /// </summary>
        public string dstl_pre { get; set; }
        /// <summary>
        /// 体重（kg）
        /// </summary>
        public decimal wt { get; set; }
        /// <summary>
        /// 腹围（cm）
        /// </summary>
        public decimal abde { get; set; }
        /// <summary>
        /// 护理观察项目名称
        /// </summary>
        public string nurscare_obsv_item_name { get; set; }
        /// <summary>
        /// 护理观察结果
        /// </summary>
        public string nurscare_obsv_rslt { get; set; }
        /// <summary>
        /// 护士姓名
        /// </summary>
        public string nurs_name { get; set; }
        /// <summary>
        /// 签字时间
        /// </summary>
        public string sign_time { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        public string vali_flag { get; set; }
    }
}
