using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
   public class Output_1901:OutputBase
    {
        public List<list1901> list;
    }
    public class list1901
    {
        //        1  type 字典类型  字符型  30  Y 
        //2  label 字典标签  字符型  50  Y 
        //3  value 字典键值  字符型  20  Y 
        //4  parent_value 父字典键值  字符型  10  Y 
        //5  sort 序号  整型  30  Y 
        //6  vali_flag 权限标识  字符型  3  Y Y 
        //7  create_user 创建账户  字符型  30  Y 
        //8  create_date 创建时间  日期型 Y 
        //9  version 版本号  整型  20  Y
        public string type { get; set; }
        public string label { get; set; }
        public string value { get; set; }
        public string parent_value { get; set; }
        public string sort { get; set; }
        public string vali_flag { get; set; }
        public string valiFlag { get; set; }
        public string create_user { get; set; }

        public string create_date { get; set; }

        public string version { get; set; }
    }
}
