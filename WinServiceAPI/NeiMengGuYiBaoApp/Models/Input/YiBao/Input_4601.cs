using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    /// <summary>
    /// 【4601】输血信息
    /// </summary>
    public class Input_4601: InputBase
    {
        public List<data4601> data { get; set; }
    }
    public class data4601
    {
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
        /// ABO 血型代码
        /// </summary>
        public string abo_code { get; set; }
        /// <summary>
        /// Rh 血型代码
        /// </summary>
        public string rh_code { get; set; }
        /// <summary>
        /// 输血性质代码
        /// </summary>
        public string bld_natu_code { get; set; }
        /// <summary>
        /// 输血 ABO 血型代码
        /// </summary>
        public string bld_abo_code { get; set; }
        /// <summary>
        /// 输血 Rh 血型代码
        /// </summary>
        public string bld_rh_code { get; set; }
        /// <summary>
        /// 输血品种代码
        /// </summary>
        public string bld_cat_code { get; set; }
        /// <summary>
        /// 输血量
        /// </summary>
        public decimal? bld_amt { get; set; }
        /// <summary>
        /// 输血量计量单位
        /// </summary>
        public string bld_amt_unt { get; set; }
        /// <summary>
        /// 输血反应类型代码
        /// </summary>
        public string bld_defs_type_code { get; set; }
        /// <summary>
        /// 输血次数
        /// </summary>
        public decimal? bld_cnt { get; set; }
        /// <summary>
        /// 输血时间
        /// </summary>
        public string bld_time { get; set; }
        /// <summary>
        /// 输血原因
        /// </summary>
        public string bld_rea { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        public string vali_flag { get; set; }

    }
}
