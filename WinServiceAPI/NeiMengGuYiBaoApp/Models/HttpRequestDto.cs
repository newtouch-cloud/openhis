using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace YiBaoInterface.Models
{
    public class HttpRequestDto
    {
        /// <summary>
        /// 缓冲区大小
        /// </summary>
        public int BUFFER_SIZE { get; set; }

        /// <summary>
        /// 缓冲区
        /// </summary>
        public byte[] BufferRead { get; set; }

        /// <summary>
        /// 保存路径
        /// </summary>
        public string SavePath { get; set; }

        /// <summary>
        /// 请求流
        /// </summary>
        public HttpWebRequest Request { get; set; }

        /// <summary>
        /// 响应流
        /// </summary>
        public HttpWebResponse Response { get; set; }

        /// <summary>
        /// 流对象
        /// </summary>
        public Stream ResponseStream { get; set; }

        /// <summary>
        /// 文件流
        /// </summary>
        public FileStream FileStream { get; set; }
    }
}
