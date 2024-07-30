using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Post
{
    public class Post_SN02:Post_Base
    {
        /// <summary>
        /// 明细账单号
        /// </summary>
        //public string mxzdh { get; set; }
        /// <summary>
        /// 明细账单号
        /// </summary>
        public string chrg_bchno { get; set; }
        /// <summary>
        /// 是否后台取值  门诊退费使用
        /// </summary>
        public string ishtqz { get; set; }
        public string jylsh { get; set; }
        /// <summary>
        /// 门诊住院标志 zy 住院 mz 门诊
        /// </summary>
        public string mzzybz { get; set; }
    }
}
