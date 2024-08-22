using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_3302 : InputBase
    {
        public catalogcompin3302 data { get; set; }
    }

    public class catalogcompin3302
    {
        public string fixmedins_hilist_id { get; set; }
        public string fixmedins_code { get; set; }
        public string list_type { get; set; }
        public string med_list_codg { get; set; }
        //1  fixmedins_code 定点医药机构编号  字符型  30  Y 
        //2 fixmedins_hilist_id定点医药机构目录编号字符型  30  Y 
        //3  list_type 目录类别  字符型  30  Y 
        //4  med_list_codg 医疗目录编码  字符型  30  Y
    }
}
