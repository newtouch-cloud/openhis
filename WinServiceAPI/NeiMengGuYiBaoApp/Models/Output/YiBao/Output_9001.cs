using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_9001 : OutputBase
    {
        public signinoutb signinoutb { get; set; }
    }
    public class signinoutb
    {
        /// <summary>
        ///  1  sign_time 签到时间  日期型 Y  yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string sign_time { get; set; }
        /// <summary>
        /// //2  sign_no 签到编号  字符型  30  Y
        /// </summary>
        public string sign_no { get; set; }
    }
}
