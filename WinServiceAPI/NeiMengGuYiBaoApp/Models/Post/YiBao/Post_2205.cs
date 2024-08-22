using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Post.YiBao
{
  public  class Post_2205
    {
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

        /// <summary>
        /// 人员编号
        /// </summary>

        public string psn_no { get; set; }
        /// <summary>
        /// 收费批次号
        /// </summary>
        public string chrg_bchno { get; set; }

        /// <summary>
        /// 医保就诊id
        /// </summary>
        public string mdtrt_id { get; set; }
    }
}
