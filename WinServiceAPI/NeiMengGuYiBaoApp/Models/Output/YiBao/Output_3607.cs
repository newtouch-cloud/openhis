using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_3607 : OutputBase
    {
        /// <summary>
        /// 定点医药机构编号|字符型|30|Y
        /// </summary>
        public string fixmedins_code {get;set; }
        /// <summary>
        /// 定点医药机构名称|字符型|255
        /// </summary>
        public string fixmedins_name{ get; set; }
        /// <summary>
        /// 结算年月 字符型 6 Y
        /// </summary>
        public string setl_ym { get; set; }
        /// <summary>
        /// 结算 ID 字符型 30 Y
        /// </summary>
        public string setl_id { get; set; }
        /// <summary>
        /// 人员编号 字符型 30 Y
        /// </summary>
        public string psn_no { get; set; }
        /// <summary>
        /// 人员姓名 字符型 50 Y
        /// </summary>
        public string psn_name { get; set; }
        /// <summary>
        /// 人员证件类型 字符型 6
        /// </summary>
        public string psn_cert_type { get; set; }
        /// <summary>
        /// 证件号码 字符型 30
        /// </summary>
        public string cert_no { get; set; }
        /// <summary>
        /// 质控结果 字符型 6 Y
        /// </summary>
        public string qltctrl_rslt { get; set; }
        /// <summary>
        /// 错误等级 字符型 6
        /// </summary>
        public string err_lv { get; set; }
        /// <summary>
        /// 回退标志 字符型 3
        /// </summary>
        public string retn_flag { get; set; }
        /// <summary>
        /// 质控版本号 字符型 60
        /// </summary>
        public string qltctrl_ver { get; set; }
        /// <summary>
        /// 分组状态 字符型 6
        /// </summary>
        public string grp_stas { get; set; }
        /// <summary>
        /// 质控结算清单结果详情信息
        /// </summary>
        public List<DetlList> detl_list { get; set; }
    }

    public class DetlList
    {
        /// <summary>
        /// 检查数据字段 字符型 100 Y
        /// </summary>
        public string exam_data_fld{get;set; }
        /// <summary>
        /// 质控校验结果 字符型 100 Y
        /// </summary>
        public string qltctrl_chkrslt{ get; set; }
        /// <summary>
        /// 错误等级 字符型 3 Y
        /// </summary>
        public string err_lv { get; set; }
        /// <summary>
        /// 回退标志 字符型 3 Y
        /// </summary>
        public string retn_flag { get; set; }
        /// <summary>
        /// 初始值 字符型 60
        /// </summary>
        public string init_val { get; set; }
    }
}
