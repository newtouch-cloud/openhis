using NeiMengGuYiBaoApp.Models.Input.YiBao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Post.YiBao
{
    public class Post_9001 : Input_9001
    {
        /// <summary>
        /// 经办人姓名
        /// </summary>
        public string opter_name { get; set; }
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string sign_no { get; set; }
    }
}
