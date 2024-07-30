using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.SQL
{
    public class Drjk_mzjzxxsc_input : SqlBase
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
        public DateTime begntime { get; set; }

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

        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// 状态 1 正常 0作废
        /// </summary>
        public int zt { get; set; }
        /// <summary>
        /// 状态操作员
        /// </summary>
        public string zt_czy { get; set; }
        /// <summary>
        /// 状态日期
        /// </summary>
        public DateTime zt_rq { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string czydm { get; set; }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime czrq { get; set; }

    }
}
