using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Post.YiBao
{
    public class Post_Download_SQL
    {
        /// <summary>
        /// 9102下载返回的地址
        /// </summary>
        public string files { get; set; }
        /// <summary>
        /// 13目录号
        /// </summary>
        public string tradiNumber { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string filename { get; set; }
        /// <summary>
        /// 列数
        /// </summary>
        public int columnNum { get; set; }
    }
}
