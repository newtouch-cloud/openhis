using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_5205 : InputBase
    {
        public data5205 data { get; set; }
    }

    public class data5205
    {
        //        5205 人员慢特病用药记录查询
        //1  psn_no 人员编号  字符型  30  Y 
        //2  begntime 开始时间  日期时间型 Y  yyyy-MM-dd HH:mm:ss
        //3  endtime 结束时间  日期时间型 yyyy-MM-dd HH:mm:s
        public string psn_no { get; set; }
        public string begntime { get; set; }
        public string endtime { get; set; }
    }
}
