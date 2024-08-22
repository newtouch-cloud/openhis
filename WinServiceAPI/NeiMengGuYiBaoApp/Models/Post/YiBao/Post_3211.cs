using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Post.YiBao
{
    public class Post_3211
    {

        /// <summary>
        ///  1  clr_begin_ymd 清算开始日期  日期型 Y
        /// </summary>
        public DateTime clr_begin_ymd { get; set; }
        /// <summary>
        /// /2  clr_end_ymd 清算结束日期  日期型
        /// </summary>
        public DateTime clr_end_ymd { get; set; }
        /// <summary>
        /// /  dtIn 数据集
        /// </summary>
        public DataTable dtIn { get; set; }
        /// <summary>
        /// /  dtIn 数据集
        /// </summary>
        public string fixmedins_code { get; set; }
    }
}
