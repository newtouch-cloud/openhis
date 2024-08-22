using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_9101 : InputBase
    {
        public fsUploadIn fsUploadIn;
    }

    public class fsUploadIn
    {
       /// <summary>
        /// 1  in  文件数据 字节数组  Y
        /// </summary>
        public object @in { get; set; }
        /// <summary>
        /// 
        /// 2  filename 文件名  字符型  200  Y
        /// </summary>
        public string filename { get; set; }
        /// <summary>
        /// 3  fixmedins_code 医药机构编号  字符型  30  Y
        /// </summary>
        public string fixmedins_code { get; set; }
    }
}
