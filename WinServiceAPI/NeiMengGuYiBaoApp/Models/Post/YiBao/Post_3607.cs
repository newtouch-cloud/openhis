using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Post.YiBao
{
    public class Post_3607
    {
        /// <summary>
        /// hisId唯一his的唯一标识
        /// </summary>
        public string hisId { get; set; }

        /// <summary>
        /// 操作员代码
        /// </summary>
        public string operatorId { get; set; }

        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string operatorName { get; set; }
        /// <summary>
        /// 结算年月  以“ yyyymm ”格式
        /// </summary>
        [Description("结算年月")]
        public string setl_ym { get; set; }

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

        /// <summary>
        /// 结算 ID  
        /// </summary>
        [Description("结算 ID")]
        public string setl_id { get; set; }

   



    }
}
