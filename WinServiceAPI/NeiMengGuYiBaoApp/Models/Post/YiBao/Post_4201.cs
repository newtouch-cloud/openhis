using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Post.YiBao
{
  public  class Post_4201
    {

        /// <summary>
        /// hisId唯一his的唯一标识
        /// </summary>
        public string hisId { get; set; }
      
        /// 操作员代码
        /// </summary>
        public string operatorId { get; set; }

        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string operatorName { get; set; }
      
       
        /// <summary>
        /// 就诊号
        /// </summary>
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 结算内码
        /// </summary>
        public string jsnm { get; set; }

    }
}
