using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_9002 : InputBase
    {
        public signOut signOut { get; set; }
    }

    public class signOut
    {
        /// <summary>
        ///   1  sign_no 签到编号  字符型  20  Y
        /// </summary>
        public string sign_no { get; set; }
        /// <summary>
        /// 2  opter_no 操作员编号  字符型  20  
        /// </summary>
        public string opter_no { get; set; }
    }
}
