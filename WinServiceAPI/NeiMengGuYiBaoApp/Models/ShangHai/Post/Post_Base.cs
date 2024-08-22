using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Post
{
    public class Post_Base
    {
        public string cardtype { get; set; }
        public string carddata { get; set; }
        /// <summary>
        /// 操作员代码
        /// </summary>
        public string operatorId { get; set; }

        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string operatorName { get; set; }
        /// <summary>
        /// hisID门诊号
        /// </summary>
        public string hisId { get; set; }
        /// <summary>
        /// 行政区划
        /// </summary>
        public string insuplc_admdvs { get; set; }

        public string orgId { get; set; }
    }
}
