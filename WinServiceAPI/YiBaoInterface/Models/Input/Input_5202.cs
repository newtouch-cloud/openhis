using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
   public class Input_5202:InputBase
    {
        public data5202 data { get; set; }
    }

    public class data5202
    {
        //        5202诊断信息查询
        //1  mdtrt_id 就诊 ID 字符型  30  Y 
        //2  psn_no 人员编号  字符型  30  

        public string mdtrt_id { get; set; }
        public string psn_no { get; set; }

    }
}
