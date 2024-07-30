using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiBaoInterface.Models.Post
{
    public class Post_Download
    {
        public string ver { get; set; }
        public string tradiNumber { get; set; }
        /// <summary>
        /// 0 目录 1 下载（9102）
        /// </summary>
        public string type { get; set; }
        public string filename { get; set; }
        public string file_qury_no { get; set; }
    }
}
