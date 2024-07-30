using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
   public class Input_3301:InputBase
    {
        public data3301 data { get; set; }
    }

    public class data3301
    {
        public string fixmedins_hilist_id { get; set; }
        public string fixmedins_hilist_name { get; set; }
        public string list_type { get; set; }
        public string med_list_codg { get; set; }
        public string begndate { get; set; }
        public string enddate { get; set; }
        public string aprvno { get; set; }
        public string dosform { get; set; }
        public string exct_cont { get; set; }
        public string item_cont { get; set; }
        public string prcunt { get; set; }
        public string spec { get; set; }
        public string pacspec { get; set; }
        public string memo { get; set; }
        //1  fixmedins_hilist_id 定点医药机构目录编号字符型  30  Y 
        //2  fixmedins_hilist_name 定点医药机构目录名称字符型  200  Y 
        //3  list_type 目录类别  字符型  30  Y 
        //4  med_list_codg 医疗目录编码  字符型  30  
        //5  begndate 开始日期  日期型 Y 
        //6  enddate 结束日期  日期型 Y 
        //7  aprvno 批准文号  字符型  30  N 
        //8  dosform 剂型  字符型  200  N 
        //9  exct_cont 除外内容  字符型  2000  N 
        //10  item_cont 项目内涵  字符型  2,000  N 
        //11  prcunt 计价单位  字符型  100  N 
        //12  spec 规格  字符型  200  N 
        //13  pacspec 包装规格  字符型  100  N 
        //14  memo 备注  字符型  500  N
    }
}
