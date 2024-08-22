using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_2203A : InputBase
    {
        public mdtrtinfo_mz mdtrtinfo;

        /// <summary>
        /// 输入-诊断信息（节点标识：diseinfo）
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现 解决待验证*/
        public List<diseinfo> diseinfo { get; set; }
    }

    public class mdtrtinfo_mz
    {
        /// <summary>
        /// 就诊 ID 字符型 30 Y
        /// </summary>
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 人员编号 字符型 30 Y
        /// </summary>
        public string psn_no { get; set; }

        /// <summary>
        /// 医疗类别 字符型 6 Y Y
        /// </summary>
        public string med_type { get; set; }

        /// <summary>
        /// 开始时间 日期时间型 Y 就诊时间
        /// </summary>
        public string begntime { get; set; }

        /// <summary>
        /// 主要病情描述 字符型 1000
        /// </summary>
        public string main_cond_dscr { get; set; }

        /// <summary>
        /// 病种编码 字符型 30 按照标准编码填写：按病种结算病种目录代码
        /// </summary>
        public string dise_codg { get; set; }

        /// <summary>
        /// 病种名称 字符型 500
        /// </summary>
        public string dise_name { get; set; }

        /// <summary>
        /// 计划生育手术类别 字符型 6 Y 生育门诊按需录入
        /// </summary>
        public string birctrl_type { get; set; }

        /// <summary>
        /// 计划生育手术或生育日期 日期型 生育门诊按需录入，yyyy-MM-dd
        /// </summary>
        public string birctrl_matn_date { get; set; }

        /// <summary>
        /// 生育类别 字符型 6
        /// </summary>
        public string matn_type { get; set; }

        /// <summary>
        /// 孕周数 数值型 2
        /// </summary>
        public int geso_val { get; set; }
        public string expContent { get; set; }
    }
    /*
    public class diseinfo
    {
        /// <summary>
        /// 诊断类别 字符型 3 Y Y
        /// </summary>
        public string diag_type { get; set; }

        /// <summary>
        /// 诊断排序号 数值型 2 Y
        /// </summary>
        public int diag_srt_no { get; set; }

        /// <summary>
        /// 诊断代码 字符型 20 Y
        /// </summary>
        public string diag_code { get; set; }

        /// <summary>
        /// 诊断名称 字符型 100 Y
        /// </summary>
        public string diag_name { get; set; }

        /// <summary>
        /// 诊断科室 字符型 50 Y
        /// </summary>
        public string diag_dept { get; set; }

        /// <summary>
        /// 诊断医生编码 字符型 30 Y
        /// </summary>
        public string dise_dor_no { get; set; }

        /// <summary>
        /// 诊断医生姓名 字符型 50 Y
        /// </summary>
        public string dise_dor_name { get; set; }

        /// <summary>
        /// 诊断时间 日期时间型 Y yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string diag_time { get; set; }

        /// <summary>
        /// 有效标志 字符型 3 Y Y
        /// </summary>
        public string vali_flag { get; set; }
    }
    */
}
