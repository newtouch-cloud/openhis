using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
  public  class Input_5302:InputBase
    {
        public data5302 data { get; set; }
    }

    public class data5302
    {
        public string psn_no { get; set; }
        public string biz_appy_type { get; set; }
        //1  psn_no 人员编号  字符型  30  Y 
        //2  biz_appy_type 业务申请类型  字符型
    }
}
