using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_5304:InputBase
    {
        public data5304 data { get; set; }
    }

    public class data5304
    {
        //        5304 转院信息查询
        //1  psn_no 人员编号  字符型  30  
        //2  begntime 开始时间  日期时间型 Yyyyy-MM-ddHH:mm:ss
        //3  endtime 结束时间  日期时间型 yyyy-MM-ddHH:mm:
        public string begntime { get; set; }

        public string endtime { get; set; }
        public string psn_no { get; set; }

    }
}
