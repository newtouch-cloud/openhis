using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_9001:InputBase
    {
        public signIn signIn { get; set; }
    }
    public class signIn
    {
        /// <summary>
        ///   1  opter_no 操作员编号  字符型  20  Y
        /// </summary>
        public string opter_no { get; set; }
        /// <summary>
        /// 2  mac 签到 MAC 地址  字符型  20  
        /// </summary>
        public string mac { get; set; }
        /// <summary>
        /// 3  ip 签到 IP 地址  字符型  20  Y
        /// </summary>
        public string ip { get; set; }
    }
}
