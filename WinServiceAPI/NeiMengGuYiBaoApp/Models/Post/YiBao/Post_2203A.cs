using NeiMengGuYiBaoApp.Models.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Post.YiBao
{



    public class Post_2203A
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

        ///<summary>
        // 操作员代码
        /// </summary>
        public string operatorId { get; set; }

        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string operatorName { get; set; }
        /// <summary>
        /// 参保地
        /// </summary>
        public string insuplc_admdvs { get; set; }

        /// <summary>
        /// hisID门诊号
        /// </summary>
        public string hisId { get; set; }
    }
}
