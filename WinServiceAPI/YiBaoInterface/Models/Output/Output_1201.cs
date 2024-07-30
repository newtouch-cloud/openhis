using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Output
{
  public  class Output_1201:OutputBase
    {
        public List<medinsinfoOutput1201> medinsinfo { get; set; }
    }
    public class medinsinfoOutput1201
    {
        public string fixmedins_code { get; set; }
        public string fixmedins_name { get; set; }
        public string uscc { get; set; }
        public string fixmedins_type { get; set; }
        public string hosp_lv { get; set; }

        //        1 fixmedins_code定点医药机构编号 字符型  12  Y 
        //2 fixmedins_name定点医药机构名称 字符型  200  Y 
        //3  uscc 统一社会信用代码  字符型  50  
        //4 fixmedins_type 定点医疗服务机构类型字符型  6  Y Y 
        //5  hosp_lv 医院等级  字符型  6  Y
    }
}
