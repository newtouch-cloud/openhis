using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_5203 : InputBase
    {
        public data5203 data { get; set; }
    }
    public class data5203
    {

        //5203结算信息查询
        public string psn_no { get; set; }
        public string setl_id { get; set; }
        public string mdtrt_id { get; set; }

        //1  psn_no 人员编号  字符型  30  Y 
        //2  setl_id 结算 ID 字符型  30  Y 
        //3  mdtrt_id 就诊 ID 字符型  30  
    }
}
