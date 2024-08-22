using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
  public  class Input_5206:InputBase
    {
        public  data5206 data { get; set;}
    }
    public class data5206
    {
        //        5206 人员累计信息查询
        //1  psn_no 人员编号  字符型  30  Y 
        //2  cum_ym 累计年月  字符型  6 
        public string psn_no { get; set; }
        public string cum_ym { get; set; }

    }
}
