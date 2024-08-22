using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Post
{
    public class Post_SK01:Post_Base
    {
        public string translsh { get; set; }

        public decimal totalexpense { get; set; }
        public string xsywlx { get; set; }
        /// <summary>
        /// 收费类型 0：挂号 1：门诊收费  2:住院收费
        /// </summary>
        public string sflx { get; set; }
        /// <summary>
        /// 1 :HIS前端传递流水号查询冲正费用 0:医保异常自动撤销
        /// </summary>
        public string cxly { get; set; }
    }
}
