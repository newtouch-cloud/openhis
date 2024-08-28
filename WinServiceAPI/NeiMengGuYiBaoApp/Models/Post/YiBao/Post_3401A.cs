using System.Collections.Generic;
using NeiMengGuYiBaoApp.Models.Input.YiBao;

namespace NeiMengGuYiBaoApp.Models.Post.YiBao
{
   public class Post_3401A
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
        /// 科室信息
        /// </summary>
        public List<string> ksCodes { get; set; }
    }
}
