﻿using NeiMengGuYiBaoApp.Models.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Post.YiBao
{
    public class Post_2208 
    {
        /// <summary>
        /// 操作员代码
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

        /// <summary>
        /// 1|结算ID|字符型|30|  |Y  |
        /// </summary> 
        public string setl_id { get; set; }

        /// <summary>
        /// 2|就诊ID|字符型|30|  |Y|
        /// </summary> 
		public string mdtrt_id { get; set; }

        /// <summary>
        /// 3|人员编号|字符型|30|  |Y|
        /// </summary> 
		public string psn_no { get; set; }
    }
}
