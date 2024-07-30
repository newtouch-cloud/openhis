using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_4104 : InputBase
    {
        public data4104 data { get; set; }
    }

    public class data4104
    {

        /// <summary>
        /// 结算年月  以“ yyyymm ”格式
        /// </summary>
        [Description("结算年月")]
        public string setl_ym { get; set; }

        /// <summary>
        /// 结算 ID  
        /// </summary>
        [Description("结算 ID")]
        public string setl_id { get; set; }

        /// <summary>
        /// 人员编号
        /// </summary>
        [Description("人员编号")]
        public string psn_no { get; set; }

        /// <summary>
        /// 错误等级
        /// </summary>
        [Description("错误等级")]
        public string err_lv { get; set; }

        /// <summary>
        /// 回退标志
        /// </summary>
        [Description("回退标志")]
        public string retn_flag { get; set; }

    /// <summary>
    /// 当前页数
    /// </summary>
    [Description("当前页数")]
        public string page_num { get; set; }

/// <summary>
/// 本页数据量
/// </summary>
[Description("本页数据量")]
        public string page_size { get; set; }

    }
}
